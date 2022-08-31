using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Entity;
using LMS.Core.Enum;
using LMS.Core.Models.QuizHistoryModels;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.RequestModels.OptionRequestModel;
using LMS.Core.Models.RequestModels.QuestionBankRequestModel;
using LMS.Core.Models.RequestModels.QuestionRequestModel;
using LMS.Core.Models.RequestModels.QuizRequestModel;
using LMS.Core.Models.RequestModels.RoleRequestModel;
using LMS.Core.Models.RequestModels.SectionRequestModel;
using LMS.Core.Models.RequestModels.SurveyOptionRequestModel;
using LMS.Core.Models.RequestModels.TemplateRequestModel;
using LMS.Core.Models.TMSResponseModel;
using LMS.Core.Models.ViewModels;
using System;
using System.Linq;

namespace LMS.Core.Models.Mapper
{
    public class AutoMapperProfile : Profile
    {
        private readonly ICurrentUserService _currentUserService;
        public AutoMapperProfile(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            #region permission role
            CreateMap<PermissionRole, PermissionRoleViewModel>();
            CreateMap<PermissionRoleRequestModel, PermissionRole>();
            CreateMap<Role, RoleViewModel>()
                .ForMember(x => x.Permissions, expr => expr.MapFrom(src => src.Permissions));
            CreateMap<RoleCreateRequestModel, Role>()
                .ForMember(x => x.Permissions, expr => expr.MapFrom(src => src.Permissions));
            CreateMap<RoleUpdateRequestModel, Role>()
            .ForMember(x => x.Permissions, expr => expr.MapFrom(src => src.Permissions));
            CreateMap<PermissionCategory[], PermissionCategoryViewModel>()
                .ForMember(x => x.Categories, expr => expr.MapFrom(src => src));
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Role, RoleViewModelWithoutPermission>();
            #endregion

            #region user
            DateTimeOffset result;
            CreateMap<UserDetailModel, User>()
                .ForMember(x => x.DateOfBirth, expr => expr.MapFrom(src =>
                                            DateTimeOffset.TryParse(src.DateOfBirthString, out result)
                                            ? result
                                            : DateTimeOffset.MinValue));
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserRoleViewModel>()
                .ForMember(x => x.Roles, expr => expr.MapFrom(src => src.Roles.Select(r => r.Role).ToList()));
            CreateMap<UserViewModel, UserRoleViewModel>();
            CreateMap<User, InstructorViewModel>();
            CreateMap<User, ManagerViewModel>();
            CreateMap<User, AttendeeSummaryViewModel>()
                .ForMember(x => x.CourseTracking, expr => expr.MapFrom(src => src.Courses.FirstOrDefault())).ReverseMap();
            CreateMap<User, AttendeeViewModel>().ReverseMap();
            #endregion

            #region course - topic
            CreateMap<Topic, TopicViewModelWithoutResource>();
            CreateMap<Topic, TopicViewModelWithResource>()
                .ForMember(x => x.TopicTracking, expr => expr.MapFrom(src => src.TopicTrackings.FirstOrDefault())).ReverseMap();
            CreateMap<TopicCreateRequestModel, Topic>();
            CreateMap<TopicUpdateRequestModel, Topic>();
            CreateMap<Course, CoursePagingViewModel>()
                .ForMember(x => x.ActionType, expr => expr.MapFrom(src => src.Users.FirstOrDefault().ActionType.ToString()))
                .ForMember(x => x.CourseTracking, expr => expr.MapFrom(src => src.Users.FirstOrDefault()))
                .ForMember(x => x.PassScore, expr => expr.MapFrom(src => src.Subject.PassScore)).ReverseMap();
            CreateMap<Course, CourseDetailViewModel>()
                .ForMember(x => x.Topics, expr => expr.MapFrom(src => src.Topics))
                .ForMember(x => x.Instructors, expr => expr.MapFrom(src => src.Users.Where(uc => uc.ActionType == ActionType.Teach).Select(u => u.User)))
                .ForMember(x => x.Monitors, expr => expr.MapFrom(src => src.Users.Where(uc => uc.ActionType == ActionType.Manage).Select(u => u.User)))
                .ForMember(x => x.CourseTracking, expr => expr.MapFrom(src => src.Users.Where(uc => uc.ActionType == ActionType.Study && uc.UserId == _currentUserService.UserId).FirstOrDefault()))
                .ForMember(x => x.PassScore, expr => expr.MapFrom(src => src.Subject.PassScore)).ReverseMap();
            CreateMap<User, AttendeeSummaryViewModel>()
                .ForMember(x => x.CourseTracking, expr => expr.MapFrom(src => src.Courses.FirstOrDefault()));
            CreateMap<TopicTracking, TopicTrackingViewModel>().ReverseMap();
            CreateMap<UserCourse, CourseTrackingViewModel>().ReverseMap();

            CreateMap<Course, CourseMarkReportViewModel>()
                .ForMember(x => x.CourseId, expr => expr.MapFrom(src => src.Id))
                .ForMember(x => x.CourseCode, expr => expr.MapFrom(src => src.Code))
                .ForMember(x => x.CourseName, expr => expr.MapFrom(src => src.Name))
                .ForMember(x => x.SubjectCode, expr => expr.MapFrom(src => src.Subject.Code))
                .ForMember(x => x.SubjectName, expr => expr.MapFrom(src => src.Subject.Name))
                .ForMember(x => x.PassScore, expr => expr.MapFrom(src => src.Subject.PassScore))
                .ForMember(x => x.Instructors, expr => expr.MapFrom(src => src.Users.Where(uc => uc.ActionType == ActionType.Teach).Select(u => u.User)))
                .ForMember(x => x.Monitors, expr => expr.MapFrom(src => src.Users.Where(uc => uc.ActionType == ActionType.Manage).Select(u => u.User)))
                .ReverseMap();

            CreateMap<User, AttendeeMarkReportViewModel>()
                .ForMember(x => x.GPA, expr => expr.MapFrom(src => src.Courses.First().FinalScore))
                .ForMember(x => x.LearningStatus, expr => expr.MapFrom(src => src.Courses.First().LearningStatus))
                .ForMember(x => x.QuizResult, expr => expr.MapFrom(src => src.Quizzes))
                .ReverseMap();

            CreateMap<Course, OwnMarkReportViewModel>()
                .ForMember(x => x.CourseId, expr => expr.MapFrom(src => src.Id))
                .ForMember(x => x.CourseCode, expr => expr.MapFrom(src => src.Code))
                .ForMember(x => x.CourseName, expr => expr.MapFrom(src => src.Name))
                .ForMember(x => x.SubjectCode, expr => expr.MapFrom(src => src.Subject.Code))
                .ForMember(x => x.SubjectName, expr => expr.MapFrom(src => src.Subject.Name))
                .ForMember(x => x.PassScore, expr => expr.MapFrom(src => src.Subject.PassScore))
                .ForMember(x => x.Instructors, expr => expr.MapFrom(src => src.Users.Where(uc => uc.ActionType == ActionType.Teach).Select(u => u.User)))
                .ForMember(x => x.Monitors, expr => expr.MapFrom(src => src.Users.Where(uc => uc.ActionType == ActionType.Manage).Select(u => u.User)))
                .ForMember(x => x.GPA, expr => expr.MapFrom(src => src.Users.Where(uc => uc.UserId == _currentUserService.UserId).First().FinalScore))
                .ForMember(x => x.LearningStatus, expr => expr.MapFrom(src => src.Users.Where(uc => uc.UserId == _currentUserService.UserId).First().LearningStatus))
                .ReverseMap();

            CreateMap<Course, CourseGradeReportViewModel>()
                .ForMember(x => x.CourseId, expr => expr.MapFrom(src => src.Id))
                .ForMember(x => x.CourseCode, expr => expr.MapFrom(src => src.Code))
                .ForMember(x => x.CourseName, expr => expr.MapFrom(src => src.Name))
                .ForMember(x => x.SubjectCode, expr => expr.MapFrom(src => src.Subject.Code))
                .ForMember(x => x.SubjectName, expr => expr.MapFrom(src => src.Subject.Name))
                .ForMember(x => x.PassScore, expr => expr.MapFrom(src => src.Subject.PassScore))
                .ForMember(x => x.FinishTime, expr => expr.MapFrom(src => src.Users.First().FinishTime))
                .ForMember(x => x.GPA, expr => expr.MapFrom(src => src.Users.First().FinalScore))
                .ForMember(x => x.LearningStatus, expr => expr.MapFrom(src => src.Users.First().LearningStatus))
                .ReverseMap();

            CreateMap<Topic, TopicWithRestrictionViewModel>()
               .ForMember(x => x.TopicId, expr => expr.MapFrom(src => src.Id))
               .ForMember(x => x.TopicName, expr => expr.MapFrom(src => src.Name))
               .ReverseMap();

            CreateMap<TopicSCORM, TopicSCORMWithoutCoreViewModel>()
               .ForMember(x => x.TopicSCORMId, expr => expr.MapFrom(src => src.Id))
               .ForMember(x => x.PathToIndex, expr => expr.MapFrom(src => src.SCORM.PathToIndex))
               .ForMember(x => x.PathToFolder, expr => expr.MapFrom(src => src.SCORM.PathToFolder))
               .ForMember(x => x.SCORMVersion, expr => expr.MapFrom(src => src.SCORM.SCORMVersion))
               .ForMember(x => x.StandAloneIndexPage, expr => expr.MapFrom(src => src.SCORM.StandAloneIndexPage))
               .ForMember(x => x.CreateTime, expr => expr.MapFrom(src => src.SCORM.CreateTime))
               .ForMember(x => x.CreateBy, expr => expr.MapFrom(src => src.SCORM.CreateBy));
            #endregion

            #region subject
            //subject detail: object sync from tms
            //convert "Re-Qualification" to SubjectType.Re_Qualification if any
            CreateMap<SubjectDetail, Subject>()
                .ForMember(x => x.Type, expr => expr.MapFrom(src =>
                    src.SubjectType.Name == "Re-Qualification" 
                    ? SubjectType.Re_Qualification 
                    : System.Enum.Parse<SubjectType>(src.SubjectType.Name)));
            CreateMap<Subject, SubjectViewModel>()
                .ForMember(x => x.Instructors, expr => expr.MapFrom(src => src.Users.Select(u => u.User)))
                .ReverseMap();

            CreateMap<Subject, SubjectViewModelWithoutSection>()
                .ForMember(x => x.Instructors, expr => expr.MapFrom(src => src.Users.Select(u => u.User)))
                .ReverseMap();
            #endregion

            #region question bank
            CreateMap<QuestionBankCreateRequestModel, QuestionBank>();
            CreateMap<QuestionBank, QuestionBankViewModel>();
            #endregion

            #region question
            CreateMap<QuestionCreateRequestModel, Question>()
                        .ForMember(x => x.Options, expr => expr.MapFrom(src => src.Options));
            CreateMap<Question, QuestionViewModel>()
                        .ForMember(x => x.Type, expr => expr.MapFrom(src => src.Type.ToString()));
            CreateMap<Question, QuestionViewModelWithoutOptions>()
                        .ForMember(x => x.Type, expr => expr.MapFrom(src => src.Type.ToString()));
            CreateMap<OptionCreateRequestModel, Option>();
            CreateMap<Option, OptionViewModel>();
            CreateMap<QuestionType[], QuestionTypeViewModel>()
                .ForMember(x => x.Types, expr => expr.MapFrom(src => src));
            CreateMap<OptionUpdateRequestModel, Option>();
            #endregion

            #region template
            CreateMap<TemplateOptionRequestModel, TemplateOption>().ReverseMap();

            //view
            CreateMap<TemplateOption, TemplateMultipleChoiceOptionViewModel>();
            CreateMap<TemplateOption, TemplateMatrixOptionViewModel>();
            CreateMap<TemplateOption, TemplateOptionViewModel>();
            CreateMap<TemplateQuestion, TemplateMatrixQuestionViewModel>();
            CreateMap<Template, TemplateViewModelWithoutQuestions>()
                .ForMember(x => x.Title, expr => expr.MapFrom(src => src.Name));
            CreateMap<Template, TemplateViewModel>()
                .ForMember(x => x.Title, expr => expr.MapFrom(src => src.Name));
            #endregion

            #region learning resource
            CreateMap<OtherLearningResource, OtherLearningResourceViewModel>();
            CreateMap<OtherLearningResource, OtherLearningResourceMovingViewModel>();
            CreateMap<SCORM, SCORMViewModel>();
            CreateMap<SCORM, ScormMovingViewModel>();
            CreateMap<TopicSCORM, TopicSCORMViewModel>()
                    .ForMember(x => x.SCORMId, expr => expr.MapFrom(src => src.SCORM.Id))
                    .ForMember(x => x.TitleFromUpload, expr => expr.MapFrom(src => src.SCORM.TitleFromUpload))
                    .ForMember(x => x.CreateTime, expr => expr.MapFrom(src => src.CreateTime))
                    .ForMember(x => x.SCORMCore, expr => expr.MapFrom(src => src.SCORMCores.FirstOrDefault()));
            CreateMap<TopicOtherLearningResource, TopicOtherLearningResourceViewModel>()
                    .ForMember(x => x.OtherLearningResourceId, expr => expr.MapFrom(src => src.OtherLearningResource.Id))
                    .ForMember(x => x.Title, expr => expr.MapFrom(src => src.OtherLearningResource.Title))
                    .ForMember(x => x.CreateTime, expr => expr.MapFrom(src => src.CreateTime))
                    .ForMember(x => x.Type, expr => expr.MapFrom(src => src.OtherLearningResource.Type))
                    .ForMember(x => x.OLRTracking, expr => expr.MapFrom(src => src.OLRTrackings.FirstOrDefault()));

            CreateMap<TopicOtherLearningResource, TopicOLRWithoutTrackingViewModel>()
                    .ForMember(x => x.TopicOtherLearningResourceId, expr => expr.MapFrom(src => src.Id))
                    .ForMember(x => x.PathToFile, expr => expr.MapFrom(src => src.OtherLearningResource.PathToFile))
                    .ForMember(x => x.Type, expr => expr.MapFrom(src => src.OtherLearningResource.Type))
                    .ReverseMap();

            CreateMap<TopicSCORM, ResourceWithRestrictionViewModel>()
                    .ForMember(x => x.TopicResourceId, expr => expr.MapFrom(src => src.Id))
                    .ForMember(x => x.Type, expr => expr.MapFrom(src => RestrictionResourceType.SCORM))
                    .ForMember(x => x.TopicResourceName, expr => expr.MapFrom(src => src.SCORMName))
                    .ForMember(x => x.CreateTime, expr => expr.MapFrom(src => src.CreateTime))
                    .ReverseMap();
            CreateMap<TopicOtherLearningResource, ResourceWithRestrictionViewModel>()
                    .ForMember(x => x.TopicResourceId, expr => expr.MapFrom(src => src.Id))
                    .ForMember(x => x.Type, expr => expr.MapFrom(src => src.OtherLearningResource.Type))
                    .ForMember(x => x.TopicResourceName, expr => expr.MapFrom(src => src.OtherLearningResourceName))
                    .ForMember(x => x.CreateTime, expr => expr.MapFrom(src => src.CreateTime))
                    .ReverseMap();
            #endregion

            #region tracking
            CreateMap<OtherLearningResourceTracking, OtherLearningResourceTrackingViewModel>();
            CreateMap<SCORMCore, SCORMCoreViewModel>();
            CreateMap<OtherLearningResourceTrackingRequestModel, OtherLearningResourceTracking>();
            CreateMap<UserCourse, LearningProgressDetailViewModel>()
                .ForMember(x => x.CourseTrackingId, expr => expr.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<OtherLearningResourceTracking, OtherLearningResourceUpdateProgressViewModel>().ReverseMap();
            #endregion

            #region survey
            CreateMap<SurveyOptionRequestModel, SurveyOption>().ReverseMap();
            CreateMap<SurveyOption, SurveyMultipleChoiceOptionViewModel>();
            CreateMap<SurveyOption, SurveyMatrixOptionViewModel>();
            CreateMap<SurveyOption, SurveyOptionViewModel>();
            CreateMap<SurveyQuestion, SurveyMatrixQuestionViewModel>();
            CreateMap<Survey, SurveyViewModelWithoutQuestions>()
                .ForMember(x => x.Title, expr => expr.MapFrom(src => src.Name));

            CreateMap<Survey, SurveyViewModel>()
                .ForMember(x => x.Title, expr => expr.MapFrom(src => src.Name));
            CreateMap<Survey, SurveyInTopicViewModel>()
                .ForMember(x => x.SurveyTracking, expr => expr.MapFrom(src => src.UserSurveys.FirstOrDefault()))
                .ReverseMap();

            CreateMap<Survey, SurveyManagementViewModel>()
               .ForMember(x => x.Title, expr => expr.MapFrom(src => src.Name))
               .ForMember(x => x.CourseCode, expr => expr.MapFrom(src => src.Topic.Course.Code))
               .ForMember(x => x.CourseName, expr => expr.MapFrom(src => src.Topic.Course.Name))
               .ForMember(x => x.ParentCode, expr => expr.MapFrom(src => src.Topic.Course.ParentCode))
               .ForMember(x => x.ParentName, expr => expr.MapFrom(src => src.Topic.Course.ParentName))
               .ForMember(x => x.TopicName, expr => expr.MapFrom(src => src.Topic.Name)).ReverseMap();

            CreateMap<UserSurvey, SurveyTrackingViewModel>()
                .ReverseMap();
            #endregion

            #region quiz
            CreateMap<QuizCreateRequestModel, Quiz>();
            CreateMap<Quiz, QuizPreviewViewModel>()
                .ForMember(x => x.Questions, expr => expr.MapFrom(src => src.Questions.Select(q => q.Question).ToList()));
            CreateMap<Quiz, QuizInTopicViewModel>()
                .ForMember(x => x.FinalResult, expr => expr.MapFrom(src => src.UserQuizzes.FirstOrDefault())).ReverseMap();
            CreateMap<QuizUpdateRequestModel, Quiz>();
            CreateMap<Question, QuestionInQuizViewModel>()
                        .ForMember(x => x.Type, expr => expr.MapFrom(src => src.Type.ToString()))
                        .ForMember(x => x.IsAvailable, expr => expr.MapFrom((src, x) =>
                        {
                            if (!src.IsActive || src.IsDeleted)
                            {
                                return false;
                            }
                            return true;
                        }))
                        .ReverseMap();
            CreateMap<QuizQuestion, QuestionInQuizViewModel>()
                        .ForMember(x => x.Order, expr => expr.MapFrom(src => src.Order)).ReverseMap();
            CreateMap<Quiz, QuizGradingInfoViewModel>()
                .ReverseMap();

            CreateMap<UserQuiz, FinalQuizResultViewModel>()
                .ReverseMap();
            CreateMap<Topic, TopicWithQuizResultViewModel>()
                .ForMember(x => x.TopicId, expr => expr.MapFrom(src => src.Id))
                .ForMember(x => x.TopicName, expr => expr.MapFrom(src => src.Name))
                .ForMember(x => x.Quizzes, expr => expr.MapFrom(src => src.Quizzes))
                .ReverseMap();

            CreateMap<Topic, TopicWithQuizViewModel>()
                .ForMember(x => x.TopicId, expr => expr.MapFrom(src => src.Id))
                .ForMember(x => x.TopicName, expr => expr.MapFrom(src => src.Name))
                .ForMember(x => x.Quizzes, expr => expr.MapFrom(src => src.Quizzes))
                .ReverseMap();

            CreateMap<Quiz, ResourceWithRestrictionViewModel>()
                    .ForMember(x => x.TopicResourceId, expr => expr.MapFrom(src => src.Id))
                    .ForMember(x => x.Type, expr => expr.MapFrom(src => RestrictionResourceType.Quiz))
                    .ForMember(x => x.TopicResourceName, expr => expr.MapFrom(src => src.Name))
                    .ForMember(x => x.CreateTime, expr => expr.MapFrom(src => src.CreateTime))
                    .ReverseMap();

            CreateMap<ResourceInTopicRestriction, RestrictionTemp>()
                .ReverseMap();
            #endregion

            #region quiz report
            CreateMap<Quiz, QuizReportViewModel>()
                .ForMember(x => x.Questions, expr => expr.MapFrom(src => src.Questions.Select(q => q.Question).ToList())).ReverseMap();
            CreateMap<Question, QuestionInQuizReportViewModel>().ReverseMap();
            CreateMap<QuestionHistoryModel, QuestionAnswerReportViewModel>()
                        .ForMember(x => x.Order, expr => expr.MapFrom(src => src.OriginalOrder)).ReverseMap();
            CreateMap<QuizAttempt, QuizAttemptReportViewModel>()
                .ForMember(x => x.QuizAttemptId, expr => expr.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<User, QuizAttemptReportViewModel>()
                .ForMember(x => x.UserId, expr => expr.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<User, StudentQuizResultViewModel>()
                .ForMember(x => x.UserId, expr => expr.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<Quiz, QuizWithResultViewModel>()
                .ForMember(x => x.QuizResult, expr => expr.MapFrom(src => src.UserQuizzes.FirstOrDefault()))
                .ReverseMap();
            #endregion

            #region user survey
            CreateMap<UserSurvey, UserSurveyViewModel>();
            CreateMap<UserSurveyDetail, UserSurveyMatrixQuestionViewModel>()
                .ForMember(x => x.UserSurveyDetailId, expr => expr.MapFrom(src => src.Id))
                .ForMember(x => x.Content, expr => expr.MapFrom(src => src.SurveyQuestion.Content));
            CreateMap<SurveyQuestion, SurveyQuestionViewModel>();
            #endregion

            #region quiz attempt
            CreateMap<Question, QuestionHistoryModel>()
                .ForMember(x => x.Options, expr => expr.MapFrom(src => src.Options)).ReverseMap();
            CreateMap<Option, OptionHistoryModel>().ReverseMap();
            CreateMap<QuizAttempt, QuizAttemptViewModel>().ReverseMap();
            CreateMap<AnswerHistoryModel, QuizAttemptViewModel>()
                .ForMember(x => x.Questions, expr => expr.MapFrom(src => src.Questions)).ReverseMap();
            CreateMap<Question, QuestionAttemptViewModel>()
                .ForMember(x => x.Options, expr => expr.MapFrom(src => src.Options)).ReverseMap();
            CreateMap<Option, OptionAttemptViewModel>().ReverseMap();
            CreateMap<QuestionHistoryModel, QuestionAttemptViewModel>()
                .ForMember(x => x.Options, expr => expr.MapFrom(src => src.Options)).ReverseMap();
            CreateMap<OptionHistoryModel, OptionAttemptViewModel>().ReverseMap();
            CreateMap<QuizAttempt, QuizResultViewModel>().ReverseMap();
            CreateMap<AnswerHistoryModel, QuizResultViewModel>().ReverseMap();
            CreateMap<UserQuiz, FinalResultViewModel>().ReverseMap();
            CreateMap<Quiz, QuizInfoViewModel>()
                .ForMember(x => x.AttemptResult, expr => expr.MapFrom(src => src.UserQuizzes.FirstOrDefault())).ReverseMap();
            CreateMap<AttemptSummaryViewModel, QuizAttempt>().ReverseMap();
            CreateMap<UserQuizViewModel, UserQuiz>()
                .ForMember(x => x.QuizAttempts, expr => expr.MapFrom(src => src.AttemptsSummary)).ReverseMap();
            #endregion

            #region survey aggregation
            CreateMap<SurveyOption, SurveyOptionAggregationViewModel>();
            CreateMap<SurveyQuestion, SurveyQuestionAggregationViewModel>();
            CreateMap<Survey, SurveyAggregationViewModel>();
            #endregion

            #region notification
            CreateMap<Notification, NotificationViewModel>()
                .ForMember(x => x.IsRead, 
                    expr => expr.MapFrom(src => src.NotificationRecipientList
                                                .Where(nr => nr.UserId == _currentUserService.UserId)
                                                .FirstOrDefault().IsRead));
            #endregion

            #region section
            CreateMap<SectionCreateRequestModel, Section>();
            CreateMap<Section, SectionViewModel>();
            CreateMap<Section, SectionWithResourcesViewModel>();
            #endregion
        }
    }
}