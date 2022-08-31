using LMS.Core.Application;
using LMS.Core.Common;
using LMS.Core.Entity;
using LMS.Core.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<RoleUser> RoleUsers { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<PermissionRole> PermissionRoles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<Subject> Subjects { get; set; }
        DbSet<UserSubject> UserSubjects { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<UserCourse> UserCourses { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<SyncLog> SyncLogs { get; set; }
        DbSet<QuestionBank> QuestionBanks { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Option> Options { get; set; }
        DbSet<OtherLearningResource> OtherLearningResources { get; set; }
        DbSet<Quiz> Quizzes { get; set; }
        DbSet<QuizAttempt> QuizAttempts { get; set; }
        DbSet<QuizQuestion> QuizQuestions { get; set; }
        DbSet<SCORM> SCORMs { get; set; }
        DbSet<Survey> Surveys { get; set; }
        DbSet<SurveyOption> SurveyOptions { get; set; }
        DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        DbSet<Template> Templates { get; set; }
        DbSet<TemplateOption> TemplateOptions { get; set; }
        DbSet<TemplateQuestion> TemplateQuestions { get; set; }
        DbSet<UserQuiz> UserQuizzes { get; set; }
        DbSet<UserSurvey> UserSurveys { get; set; }
        DbSet<UserSurveyDetail> UserSurveyDetails { get; set; }
        DbSet<TopicSCORM> TopicSCORMs { get; set; }
        DbSet<SCORMCore> SCORMCores { get; set; }
        DbSet<SCORMCommentFromLearner> SCORMCommentsFromLearner { get; set; }
        DbSet<SCORMCommentFromLms> SCORMCommentsFromLms { get; set; }
        DbSet<SCORMObjective> SCORMObjectives { get; set; }
        DbSet<SCORMInteraction> SCORMInteractions { get; set; }
        DbSet<SCORMInteractionObjective> SCORMInteractionObjectives { get; set; }
        DbSet<SCORMInteractionCorrectResponse> SCORMInteractionCorrectResponses { get; set; }
        DbSet<SCORMLearnerPreference> SCORMLearnerPreferences { get; set; }
        DbSet<TopicOtherLearningResource> TopicOtherLearningResources { get; set; }
        DbSet<OtherLearningResourceTracking> OtherLearningResourceTracking { get; set; }
        DbSet<SCORMNavigation> SCORMNavigations { get; set; }
        DbSet<TopicTracking> TopicTrackings { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<NotificationRecipient> NotificationRecipients { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Mail> Mails { get; set; }
        DbSet<MailRecipient> MailRecipients { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpContextAccessor accessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService, IHttpContextAccessor accessor) : base(options)
        {
            _currentUserService = currentUserService;
            this.accessor = accessor;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {              
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateBy = _currentUserService.UserId;
                    entry.Entity.CreateTime = DateTimeOffset.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    string method = accessor.HttpContext.Request.Method;
                    if ("DELETE".Equals(method))
                    {
                        entry.Entity.DeleteBy = _currentUserService.UserId;
                        entry.Entity.DeleteTime = DateTimeOffset.Now;
                    }
                    else
                    {
                        entry.Entity.UpdateBy = _currentUserService.UserId;
                        entry.Entity.UpdateTime = DateTimeOffset.Now;
                    }
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<UserSubject> UserSubjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<SyncLog> SyncLogs { get; set; }
        public DbSet<QuestionBank> QuestionBanks { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OtherLearningResource> OtherLearningResources { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<SCORM> SCORMs { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateOption> TemplateOptions { get; set; }
        public DbSet<TemplateQuestion> TemplateQuestions { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }
        public DbSet<UserSurvey> UserSurveys { get; set; }
        public DbSet<UserSurveyDetail> UserSurveyDetails { get; set; }
        public DbSet<TopicSCORM> TopicSCORMs { get; set; }
        public DbSet<SCORMCore> SCORMCores { get; set; }
        public DbSet<SCORMCommentFromLearner> SCORMCommentsFromLearner { get; set; }
        public DbSet<SCORMCommentFromLms> SCORMCommentsFromLms { get; set; }
        public DbSet<SCORMObjective> SCORMObjectives { get; set; }
        public DbSet<SCORMInteraction> SCORMInteractions { get; set; }
        public DbSet<SCORMInteractionObjective> SCORMInteractionObjectives { get; set; }
        public DbSet<SCORMInteractionCorrectResponse> SCORMInteractionCorrectResponses { get; set; }
        public DbSet<SCORMLearnerPreference> SCORMLearnerPreferences { get; set; }
        public DbSet<TopicOtherLearningResource> TopicOtherLearningResources { get; set; }
        public DbSet<OtherLearningResourceTracking> OtherLearningResourceTracking { get; set; }
        public DbSet<SCORMNavigation> SCORMNavigations { get; set; }
        public DbSet<TopicTracking> TopicTrackings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationRecipient> NotificationRecipients { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailRecipient> MailRecipients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(p =>
            {
                //constraint unique
                p.HasIndex(p => p.Id).IsUnique();

                p.Property(p => p.Category).HasConversion(pc => pc.ToString(),
                    s => (PermissionCategory)Enum.Parse(typeof(PermissionCategory), s));

                //seed data
                p.HasData(DataSeeding.SeedPermissions());
            });

            modelBuilder.Entity<Role>(r =>
            {
                //constraint unique
                r.HasIndex(p => p.Id).IsUnique();

                r.Property(r => r.IsActive).HasDefaultValueSql("true");

                r.Ignore(r => r.DeleteTime);
                r.Ignore(r => r.DeleteBy);

                //seed data
                r.HasData(DataSeeding.SeedRoles());
            });

            modelBuilder.Entity<PermissionRole>(pr =>
            {
                pr.HasIndex(pr => pr.Id).IsUnique();
                pr.HasIndex(pr => new { pr.RoleId, pr.PermissionId }).IsUnique();

                pr.HasOne<Role>(pr => pr.Role)
                   .WithMany(p => p.Permissions)
                   .HasForeignKey(pr => pr.RoleId);

                //seed data
                pr.HasData(DataSeeding.SeedPermissionRoles());
            });

            modelBuilder.Entity<User>(u =>
            {
                u.HasIndex(u => u.Id).IsUnique();

                u.Property(u => u.DateOfJoin).HasDefaultValueSql("CURRENT_TIMESTAMP(0)");
                u.Property(u => u.IsActiveInLMS).HasDefaultValue(true);
                u.Property(u => u.IsActive).HasDefaultValue(true);
                u.Property(u => u.IsDeleted).HasDefaultValue(false);

                u.Ignore(u => u.CreateTime);
                u.Ignore(u => u.CreateBy);
                u.Ignore(u => u.DeleteTime);
                u.Ignore(u => u.DeleteBy);
            });

            modelBuilder.Entity<RoleUser>(t =>
            {
                t.HasIndex(t => t.Id).IsUnique();
                t.HasIndex(t => new { t.RoleId, t.UserId }).IsUnique();

                t.Ignore(t => t.UpdateTime);
                t.Ignore(t => t.UpdateBy);
                t.Ignore(t => t.DeleteTime);
                t.Ignore(t => t.DeleteBy);
            });

            modelBuilder.Entity<RefreshToken>().HasIndex(rt => rt.Id).IsUnique();

            modelBuilder.Entity<Notification>().HasIndex(n => n.Id).IsUnique();

            modelBuilder.Entity<NotificationRecipient>(nr =>
            {
                nr.HasIndex(n => n.Id).IsUnique();
                nr.HasIndex(nr => new { nr.UserId, nr.NotificationId }).IsUnique();
            });

            modelBuilder.Entity<Mail>().HasIndex(n => n.Id).IsUnique();

            modelBuilder.Entity<MailRecipient>(mr =>
            {
                mr.HasIndex(mr => mr.Id).IsUnique();
                mr.HasIndex(mr => new { mr.UserId, mr.MailId }).IsUnique();
            });

            modelBuilder.Entity<Course>(c =>
            {
                c.HasIndex(c => c.Id).IsUnique();

                c.Property(c => c.IsActive).HasDefaultValue(true);
                c.Property(c => c.IsDeleted).HasDefaultValue(false);

                c.Property(c => c.Type).HasConversion(ct => ct.ToString(),
                    ct => (CourseType)Enum.Parse(typeof(CourseType), ct));

                c.Ignore(c => c.CreateTime);
                c.Ignore(c => c.CreateBy);
                c.Ignore(c => c.DeleteTime);
                c.Ignore(c => c.DeleteBy);
            });

            modelBuilder.Entity<UserCourse>(uc =>
            {
                uc.HasIndex(uc => uc.Id).IsUnique();

                uc.Property(uc => uc.ActionType).HasConversion(at => at.ToString(),
                    at => (ActionType)Enum.Parse(typeof(ActionType), at));
                uc.Property(uc => uc.LearningStatus).HasConversion(at => at.ToString(),
                    at => (LearningStatus)Enum.Parse(typeof(LearningStatus), at));
            });

            modelBuilder.Entity<Topic>(t =>
            {
                t.HasIndex(t => t.Id).IsUnique();

                t.Ignore(t => t.DeleteTime);
                t.Ignore(t => t.DeleteBy);
                t.Property(p => p.CreateTime).HasDefaultValueSql("CURRENT_TIMESTAMP(0)");
            });

            modelBuilder.Entity<Quiz>(q =>
            {
                q.HasIndex(q => q.Id).IsUnique();

                q.Ignore(q => q.DeleteTime);
                q.Ignore(q => q.DeleteBy);

                q.Property(q => q.GradingMethod).HasConversion(qt => qt.ToString(),
                    qt => (GradingMethodType)Enum.Parse(typeof(GradingMethodType), qt));
            });

            modelBuilder.Entity<UserQuiz>(uq =>
            {
                uq.HasIndex(uq => uq.Id).IsUnique();
                uq.HasIndex(uq => new { uq.UserId, uq.QuizId }).IsUnique();

                uq.Property(q => q.Status).HasConversion(qa => qa.ToString(),
                    qa => (CompletionLevelType)Enum.Parse(typeof(CompletionLevelType), qa));
            });

            modelBuilder.Entity<QuizAttempt>(qt =>
            {
                qt.HasIndex(qt => qt.Id).IsUnique();

                qt.Property(q => q.Status).HasConversion(qa => qa.ToString(),
                    qa => (CompletionLevelType)Enum.Parse(typeof(CompletionLevelType), qa));
            });

            modelBuilder.Entity<QuizQuestion>(qq =>
            {
                qq.HasIndex(qq => qq.Id).IsUnique();
                qq.HasIndex(qq => new { qq.QuizId, qq.QuestionId }).IsUnique();
            });

            modelBuilder.Entity<Question>(q =>
            {
                q.HasIndex(q => q.Id).IsUnique();

                q.Property(q => q.IsActive).HasDefaultValue(true);
                q.Property(q => q.IsDeleted).HasDefaultValue(false);

                q.Property(q => q.Type).HasConversion(qt => qt.ToString(),
                    qt => (QuestionType)Enum.Parse(typeof(QuestionType), qt));
            });

            modelBuilder.Entity<Option>().HasIndex(o => o.Id).IsUnique();

            modelBuilder.Entity<QuestionBank>(qb =>
            {
                qb.HasIndex(qb => qb.Id).IsUnique();

                qb.Ignore(qb => qb.DeleteTime);
                qb.Ignore(qb => qb.DeleteBy);
            });

            modelBuilder.Entity<Subject>(s =>
            {
                s.HasIndex(s => s.Id).IsUnique();

                s.Property(s => s.IsActive).HasDefaultValue(true);
                s.Property(s => s.IsDeleted).HasDefaultValue(false);

                s.Property(s => s.Type).HasConversion(st => st.ToString(),
                    st => (SubjectType)Enum.Parse(typeof(SubjectType), st));

                s.Ignore(s => s.CreateTime);
                s.Ignore(s => s.CreateBy);
                s.Ignore(s => s.DeleteTime);
                s.Ignore(s => s.DeleteBy);
            });

            modelBuilder.Entity<UserSubject>(us =>
            {
                us.HasIndex(s => s.Id).IsUnique();
                us.HasIndex(us => new { us.UserId, us.SubjectId }).IsUnique();
            });

            modelBuilder.Entity<OtherLearningResource>(olr =>
            {
                olr.HasIndex(olr => olr.Id).IsUnique();

                olr.Property(olr => olr.IsDeleted).HasDefaultValue(false);

                olr.Ignore(olr => olr.UpdateTime);
                olr.Ignore(olr => olr.UpdateBy);

                olr.Property(q => q.Type).HasConversion(qa => qa.ToString(),
                    qa => (LearningResourceType)Enum.Parse(typeof(LearningResourceType), qa));
            });

            modelBuilder.Entity<TopicOtherLearningResource>(tolr =>
            {
                tolr.HasIndex(tolr => tolr.Id).IsUnique();
                tolr.HasIndex(tolr => new { tolr.OtherLearningResourceId, tolr.TopicId }).IsUnique();

                tolr.Ignore(tolr => tolr.DeleteTime);
                tolr.Ignore(tolr => tolr.DeleteBy);

                tolr.Property(tolr => tolr.CompletionThreshold).HasDefaultValue(0.8);
            });

            modelBuilder.Entity<SCORM>(s =>
            {
                s.HasIndex(s => s.Id).IsUnique();

                s.Property(s => s.IsDeleted).HasDefaultValue(false);

                s.Ignore(s => s.UpdateTime);
                s.Ignore(s => s.UpdateBy);
            });

            modelBuilder.Entity<TopicSCORM>(ts =>
            {
                ts.HasIndex(ts => ts.Id).IsUnique();
                ts.HasIndex(ts => new { ts.SCORMId, ts.TopicId }).IsUnique();

                ts.Ignore(ts => ts.UpdateTime);
                ts.Ignore(ts => ts.UpdateBy);
                ts.Ignore(ts => ts.DeleteTime);
                ts.Ignore(ts => ts.DeleteBy);
            });

            modelBuilder.Entity<OtherLearningResourceTracking>(olrt =>
            {
                olrt.HasIndex(olrt => olrt.Id).IsUnique();
                olrt.HasIndex(olrt => new { olrt.LearnerId, olrt.TopicOtherLearningResourceId }).IsUnique();
            });

            modelBuilder.Entity<SCORMCore>(sc =>
            {
                sc.HasIndex(sc => sc.Id).IsUnique();
                sc.HasIndex(sc => new { sc.LearnerId, sc.TopicSCORMId }).IsUnique();

                sc.HasOne(c => c.LearnerPreference).WithOne(lp => lp.SCORMCore)
                    .HasForeignKey<SCORMLearnerPreference>(lp => lp.SCORMCoreId);
                sc.HasOne(c => c.Navigation).WithOne(lp => lp.SCORMCore)
                    .HasForeignKey<SCORMNavigation>(lp => lp.SCORMCoreId);
            });

            modelBuilder.Entity<SCORMLearnerPreference>().HasIndex(slp => slp.SCORMCoreId).IsUnique();

            modelBuilder.Entity<SCORMObjective>().HasIndex(so => so.Id).IsUnique();

            modelBuilder.Entity<SCORMNavigation>().HasIndex(sn => sn.SCORMCoreId).IsUnique();

            modelBuilder.Entity<SCORMCommentFromLearner>().HasIndex(scfl => scfl.Id).IsUnique();

            modelBuilder.Entity<SCORMCommentFromLms>().HasIndex(scfl => scfl.Id).IsUnique();

            modelBuilder.Entity<SCORMInteraction>().HasIndex(si => si.Id).IsUnique();

            modelBuilder.Entity<SCORMInteractionCorrectResponse>().HasIndex(sicr => sicr.Id).IsUnique();

            modelBuilder.Entity<SCORMInteractionObjective>().HasIndex(sio => sio.Id).IsUnique();

            modelBuilder.Entity<Template>(t =>
            {
                t.HasIndex(t => t.Id).IsUnique();

                t.Property(t => t.IsActive).HasDefaultValue(true);
                t.Ignore(t => t.DeleteTime);
                t.Ignore(t => t.DeleteBy);
            });

            modelBuilder.Entity<TemplateQuestion>(tq =>
            {
                tq.HasIndex(tq => tq.Id).IsUnique();

                tq.Property(tq => tq.Type).HasConversion(sqt => sqt.ToString(),
                    sqt => (SurveyQuestionType)Enum.Parse(typeof(SurveyQuestionType), sqt));
            });

            modelBuilder.Entity<TemplateOption>().HasIndex(to => to.Id).IsUnique();

            modelBuilder.Entity<Survey>(s =>
            {
                s.HasIndex(s => s.Id).IsUnique();

                s.Ignore(s => s.DeleteTime);
                s.Ignore(s => s.DeleteBy);
            });

            modelBuilder.Entity<SurveyQuestion>(sq =>
            {
                sq.HasIndex(sq => sq.Id).IsUnique();

                sq.Property(sq => sq.Type).HasConversion(sqt => sqt.ToString(),
                    sqt => (SurveyQuestionType)Enum.Parse(typeof(SurveyQuestionType), sqt));
            });

            modelBuilder.Entity<SurveyOption>().HasIndex(so => so.Id).IsUnique();

            modelBuilder.Entity<UserSurvey>(us =>
            {
                us.HasIndex(us => us.Id).IsUnique();
                us.HasIndex(us => new { us.UserId, us.SurveyId }).IsUnique();
            });

            modelBuilder.Entity<UserSurveyDetail>(usd =>
            {
                usd.HasIndex(usd => usd.Id).IsUnique();
                usd.HasIndex(usd => new { usd.SurveyQuestionId, usd.UserSurveyId }).IsUnique();
            });

            modelBuilder.Entity<TopicTracking>(tt =>
            {
                tt.HasIndex(tt => tt.Id).IsUnique();
                tt.HasIndex(tt => new { tt.UserId, tt.TopicId }).IsUnique();
            });

            modelBuilder.Entity<SyncLog>(sl =>
            {
                sl.HasIndex(sl => sl.Id).IsUnique();

                sl.Property(sl => sl.StartTime).HasDefaultValueSql("CURRENT_TIMESTAMP(0)");
            });

            modelBuilder.Entity<Section>(s =>
            {
                s.HasIndex(s => s.Id).IsUnique();

                s.Property(s => s.IsDeleted).HasDefaultValue(false);
            });
        }
    }
}