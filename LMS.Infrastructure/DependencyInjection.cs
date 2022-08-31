using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Models.Mapper;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IService;
using LMS.Infrastructure.IServices;
using LMS.Infrastructure.Repositories;
using LMS.Infrastructure.Services;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection SetupLMSBusinessService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                ).UseSnakeCaseNamingConvention();
                option.ConfigureWarnings(wc => wc.Ignore(RelationalEventId.BoolWithDefaultWarning));
            });
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile(provider.GetService<ICurrentUserService>()));
            }).CreateMapper());
            services.ConfigDependencyInjection(configuration);
            return services;
        }
        private static void ConfigDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddServices(configuration);
            services.AddHttpClient<HttpClient>(StringUtils.ClientString, c =>
            {
                c.BaseAddress = new Uri("https://smapi.vjaa.edu.vn/");
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionRoleRepository, PermissionRoleRepository>();
            services.AddScoped<IRoleUserRepository, RoleUserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<TMSRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IQuestionBankRepository, QuestionBankRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IOtherLearningResourceRepository, OtherLearningResourceRepository>();
            services.AddScoped<ISCORMRepository, SCORMRepository>();
            services.AddScoped<ISyncLogRepository, SyncLogRepository>();
            services.AddScoped<IOtherLearningResourceTrackingRepository, OtherLearningResourceTrackingRepository>();
            services.AddScoped<ISCORMCoreRepository, SCORMCoreRepository>();
            services.AddScoped<ITopicOtherLearningResourceRepository, TopicOtherLearningResourceRepository>();
            services.AddScoped<ITopicSCORMRepository, TopicSCORMRepository>();
            services.AddScoped<IUserCourseRepository, UserCourseRepository>();
            services.AddScoped<ISCORMObjectiveRepository, SCORMObjectiveRepository>();
            services.AddScoped<ISCORMCommentFromLearnerRepository, SCORMCommentFromLearnerRepository>();
            services.AddScoped<ISCORMCommentFromLMSRepository, SCORMCommentFromLMSRepository>();
            services.AddScoped<ISCORMLearnerPreferenceRepository, SCORMLearnerPreferenceRepository>();
            services.AddScoped<ISCORMInteractionRepository, SCORMInteractionRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISCORMInteractionObjectiveRepository, SCORMInteractionObjectiveRepository>();
            services.AddScoped<ISCORMInteractionCorrectResponseRepository, SCORMInteractionCorrectResponseRepository>();
            services.AddScoped<ISCORMNavigationRepository, SCORMNavigationRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuizQuestionRepository, QuizQuestionRepository>();
            services.AddScoped<IUserSurveyRepository, UserSurveyRepository>();
            services.AddScoped<IUserSurveyDetailRepository, UserSurveyDetailRepository>();
            services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
            services.AddScoped<ISurveyOptionRepository, SurveyOptionRepository>();
            services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();
            services.AddScoped<IUserQuizRepository, UserQuizRepository>();
            services.AddScoped<ITopicTrackingRepository, TopicTrackingRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationRecipientRepository, NotificationRecipientRepository>();
            services.AddScoped<IUserSubjectRepository, UserSubjectRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IMailRepository, MailRepository>();
            services.AddScoped<IMailRecipientRepository, MailRecipientRepository>();
        }

        private static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPermissionCategoryService, PermissionCategoryService>();
            services.AddScoped<ITMSService, TMSService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddTransient<IBackgroundTaskService, BackgroundTaskService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionBankService, QuestionBankService>();
            services.AddScoped<IQuestionTypeService, QuestionTypeService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IOtherLearningResourceService, OtherLearningResourceService>();
            services.AddScoped<ISCORMService, SCORMService>();
            services.AddScoped<ITrackingScormService, TrackingScormService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IUserSurveyService, UserSurveyService>();
            services.AddScoped<ITrackingOtherLearningResourseService, TrackingOtherLearningResourseService>();
            services.AddScoped<IQuizAttemptService, QuizAttemptService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserCourseService, UserCourseService>();
            services.AddScoped<ISectionService, SectionService>();
        }
    }
}
