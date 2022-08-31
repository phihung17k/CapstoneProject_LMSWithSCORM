using Hangfire;
using Hangfire.PostgreSql;
using LMS.API.Configuration;
using LMS.API.Permission;
using LMS.Core.Application;
using LMS.Infrastructure;
using LMS.API.Hubs;
using LMS.Infrastructure.Services;
using LMS.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;
using System;
using System.IO;
using System.Text;
using LMS.Core.Models.MailModels;
using LMS.API.Jobs;
using Newtonsoft.Json;

namespace LMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var emailConfig = Configuration.GetSection("EmailConfig").Get<EmailConfig>();
            services.AddSingleton(emailConfig);
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                  .AddSerilog(new LoggerConfiguration()
                        .WriteTo
                        .File(Path.Combine(Environment.CurrentDirectory, "Configuration", "log.txt"),
                                           rollingInterval: RollingInterval.Hour)
                        .CreateLogger())
                  .AddConsole();

                builder.AddDebug();
            });

            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.SetupLMSBusinessService(Configuration);

            services.AddHttpContextAccessor();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddTransient<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            services.AddTransient<QuizJob>();
            services.AddTransient<SurveyNotificationJob>();
            services.AddTransient<CourseJob>();

            services.AddControllers().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            })
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    option.SerializerSettings.Converters.Add(new StringEnumConverter());
                }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LMS.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                c.SchemaFilter<EnumSchemaFilter>();
            });

            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]);
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                    };
                }
            );
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", 
                                        "http://lms.hisoft.vn", 
                                        "https://lms.hisoft.vn",
                                        "https://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection"))
                .UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            services.AddHangfireServer();
            services.AddRouting(option => option.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.ConfigureExceptionHandler(logger, env.IsDevelopment());
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();


            app.UseRouting();

            app.UseCors("ClientPermission");

            app.UseAuthentication();

            app.UseAuthorization();



            var accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            ConfigurationUtils.Init(Configuration, accessor);
            var filesFolderPath = ConfigurationUtils.FilesFolderPath;
            if (!Directory.Exists(filesFolderPath))
            {
                Directory.CreateDirectory(filesFolderPath);
            }
            var fileProvider = new PhysicalFileProvider(filesFolderPath);
            var requestPath = "/files";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = requestPath,
                //OnPrepareResponse = ctx =>
                //{
                //    var identity = ctx.Context.User.Identity as ClaimsIdentity;
                //    var permissions = identity.Claims.Where(c => c.Type == PermissionConstants.ClaimType && c.Value == PermissionConstants.Course.ViewContentOfLearningResources).FirstOrDefault();
                //    if (permissions == null)
                //    {
                //        // respond HTTP 401 Unauthorized.
                //        ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //        ctx.Context.Response.ContentLength = 0;
                //        ctx.Context.Response.Body = Stream.Null;
                //    }
                //}
            });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
                endpoints.MapHub<NotificationHub>("/LMSHub");
            });
        }
    }
}
