using LMS.Core.Entity;
using LMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Core.Common
{
    public class DataSeeding
    {
        public static List<Permission> SeedPermissions()
        {
            int i = 1;
            List<Permission> seedingList = new()
            {
                new Permission
                {
                    Id = i++,
                    Name = "View profile",
                    Code = nameof(PermissionConstants.BasePermission.ViewProfile),
                    Description = "View profile",
                    Category = PermissionCategory.BasePermission
                },
                new Permission
                {
                    Id = i++,
                    Name = "Edit profile",
                    Code = nameof(PermissionConstants.BasePermission.EditProfile),
                    Description = "Edit profile",
                    Category = PermissionCategory.BasePermission
                },
                new Permission
                {
                    Id = i++,
                    Name = "Receive Notification",
                    Code = nameof(PermissionConstants.BasePermission.ReceiveNotification),
                    Description = "Receive Notification",
                    Category = PermissionCategory.BasePermission
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of assigned courses",
                    Code = nameof(PermissionConstants.Course.ViewAssignedCoursesList),
                    Description = "View assigned courses",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View all of courses",
                    Code = nameof(PermissionConstants.Course.ViewAllCourses),
                    Description = "View all of courses in system",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of course",
                    Code = nameof(PermissionConstants.Course.ViewDetailOfCourse),
                    Description = "View detail of course include description, outline, topics",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update course",
                    Code = nameof(PermissionConstants.Course.UpdateCourse),
                    Description = "Update course",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Create topic",
                    Code = nameof(PermissionConstants.Course.CreateTopic),
                    Description = "Create topic in course",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update topic",
                    Code = nameof(PermissionConstants.Course.UpdateTopic),
                    Description = "Update topic in course",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete topic",
                    Code = nameof(PermissionConstants.Course.DeleteTopic),
                    Description = "Delete topic in course",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Add a new learning resource",
                    Code = nameof(PermissionConstants.Course.AddLearningResource),
                    Description = "Add a new learning resource in topic",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update learning resource",
                    Code = nameof(PermissionConstants.Course.UpdateLearningResource),
                    Description = "Update learning resource",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete the learning resource",
                    Code = nameof(PermissionConstants.Course.DeleteLearningResource),
                    Description = "Delete the learning resource in topic",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View content of learning resource",
                    Code = nameof(PermissionConstants.Course.ViewContentOfLearningResources),
                    Description = "View content of learning resource",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list attendees in course",
                    Code = nameof(PermissionConstants.Course.ViewAttendeesList),
                    Description = "View list attendees in course",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View summary of quiz results",
                    Code = nameof(PermissionConstants.Course.ViewSummaryOfQuizResults),
                    Description = "View summary of quiz results of students",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of student’s quiz result",
                    Code = nameof(PermissionConstants.Course.ViewDetailOfQuizResultOfStudent),
                    Description = "View detail of student’s quiz result",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of survey results in all courses",
                    Code = nameof(PermissionConstants.Course.ViewListOfSurveyResultsInAllCourses),
                    Description = "View list of survey results of students in all courses",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of survey results in assigned courses",
                    Code = nameof(PermissionConstants.Course.ViewListOfSurveyResultsInAssignedCourses),
                    Description = "View list of survey results of students in assigned courses",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of survey result",
                    Code = nameof(PermissionConstants.Course.ViewDetailOfSurveyResult),
                    Description = "View detail of survey result (responses analysis) of attendees",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View summary of survey result",
                    Code = nameof(PermissionConstants.Course.ViewSummaryOfSurveyResults),
                    Description = "View summary of survey result of attendees",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View summary of student’s learning process",
                    Code = nameof(PermissionConstants.Course.ViewSummaryOfLearningProcessOfStudent),
                    Description = "View summary of student’s learning process",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of student’s learning process",
                    Code = nameof(PermissionConstants.Course.ViewDetailOfLearningProcessOfStudent),
                    Description = "View detail of student’s learning process",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Create quiz",
                    Code = nameof(PermissionConstants.Course.CreateQuiz),
                    Description = "Create quiz in topic",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update quiz",
                    Code = nameof(PermissionConstants.Course.UpdateQuiz),
                    Description = "Update quiz in topic",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete quiz",
                    Code = nameof(PermissionConstants.Course.DeleteQuiz),
                    Description = "Delete quiz in topic",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Attempt and Re-attempt quiz",
                    Code = nameof(PermissionConstants.Course.AttemptAndReattemptQuiz),
                    Description = "Attempt and Re-attempt quiz",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Review your own attempts",
                    Code = nameof(PermissionConstants.Course.ReviewYourOwnAttempts),
                    Description = "Review your own attempts",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Preview quiz",
                    Code = nameof(PermissionConstants.Course.PreviewQuiz),
                    Description = "Preview quiz",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Add survey",
                    Code = nameof(PermissionConstants.Course.AddSurveyFromTemplate),
                    Description = "Add survey from template",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update survey",
                    Code = nameof(PermissionConstants.Course.UpdateSurvey),
                    Description = "Update survey",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete survey",
                    Code = nameof(PermissionConstants.Course.DeleteSurvey),
                    Description = "Delete survey",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Do and edit survey",
                    Code = nameof(PermissionConstants.Course.DoAndEditSurvey),
                    Description = "Do and edit survey",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "Preview survey",
                    Code = nameof(PermissionConstants.Course.PreviewSurvey),
                    Description = "Preview survey",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View student mark report",
                    Code = nameof(PermissionConstants.Course.ViewStudentMarkReport),
                    Description = "View student mark report",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View own mark report",
                    Code = nameof(PermissionConstants.Course.ViewOwnMarkReport),
                    Description = "View own mark report",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of course's grades",
                    Code = nameof(PermissionConstants.Course.ViewCoursesGrades),
                    Description = "View list of course's grades",
                    Category = PermissionCategory.Course
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of survey templates",
                    Code = nameof(PermissionConstants.SurveyTemplate.ViewSurveyTemplatesList),
                    Description = "View list of survey templates",
                    Category = PermissionCategory.SurveyTemplate
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of survey template",
                    Code = nameof(PermissionConstants.SurveyTemplate.ViewDetailOfSurveyTemplate),
                    Description = "View detail of survey template",
                    Category = PermissionCategory.SurveyTemplate
                },
                new Permission
                {
                    Id = i++,
                    Name = "Create a new survey template",
                    Code = nameof(PermissionConstants.SurveyTemplate.CreateSurveyTemplate),
                    Description = "Create a new survey template",
                    Category = PermissionCategory.SurveyTemplate
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update the survey template",
                    Code = nameof(PermissionConstants.SurveyTemplate.UpdateSurveyTemplate),
                    Description = "Update the survey template",
                    Category = PermissionCategory.SurveyTemplate
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete the survey template",
                    Code = nameof(PermissionConstants.SurveyTemplate.DeleteSurveyTemplate),
                    Description = "Delete the survey template",
                    Category = PermissionCategory.SurveyTemplate
                },
                new Permission
                {
                    Id = i++,
                    Name = "Create question bank",
                    Code = nameof(PermissionConstants.QuestionBank.CreateQuestionBank),
                    Description = "Create question bank",
                    Category = PermissionCategory.QuestionBank
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update question bank",
                    Code = nameof(PermissionConstants.QuestionBank.UpdateQuestionBank),
                    Description = "Update question bank",
                    Category = PermissionCategory.QuestionBank
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete question bank",
                    Code = nameof(PermissionConstants.QuestionBank.DeleteQuestionBank),
                    Description = "Delete question bank",
                    Category = PermissionCategory.QuestionBank
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of questions in a question bank",
                    Code = nameof(PermissionConstants.Question.ViewQuestionsList),
                    Description = "View list of questions in a question bank",
                    Category = PermissionCategory.Question
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of a question",
                    Code = nameof(PermissionConstants.Question.ViewDetailOfQuestion),
                    Description = "View detail of a question",
                    Category = PermissionCategory.Question
                },
                new Permission
                {
                    Id = i++,
                    Name = "Create question",
                    Code = nameof(PermissionConstants.Question.CreateQuestion),
                    Description = "Create question",
                    Category = PermissionCategory.Question
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update question",
                    Code = nameof(PermissionConstants.Question.UpdateQuestion),
                    Description = "Update question",
                    Category = PermissionCategory.Question
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete question",
                    Code = nameof(PermissionConstants.Question.DeleteQuestion),
                    Description = "Delete question",
                    Category = PermissionCategory.Question
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of subjects",
                    Code = nameof(PermissionConstants.Subject.ViewSubjectsList),
                    Description = "View list of subjects",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of assigned subjects",
                    Code = nameof(PermissionConstants.Subject.ViewAssignedSubjectsList),
                    Description = "View list of assigned subjects",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of subject",
                    Code = nameof(PermissionConstants.Subject.ViewDetailOfSubject),
                    Description = "View detail of subject",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "Create section",
                    Code = nameof(PermissionConstants.Subject.CreateSection),
                    Description = "Create new section in subject",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update section",
                    Code = nameof(PermissionConstants.Subject.UpdateSection),
                    Description = "Update section in subject",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete section",
                    Code = nameof(PermissionConstants.Subject.DeleteSection),
                    Description = "Delete section in subject",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of learning resources in a subject",
                    Code = nameof(PermissionConstants.Subject.ViewLearningResourcesList),
                    Description = "View list of learning resources in a subject",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "View content of learning resource in a subject",
                    Code = nameof(PermissionConstants.Subject.ViewContentOfLearningResourcesInSubject),
                    Description = "View content of learning resource in a subject",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "Add a new learning resource",
                    Code = nameof(PermissionConstants.Subject.AddLearningResource),
                    Description = "Add a new learning resource",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete the learning resource",
                    Code = nameof(PermissionConstants.Subject.DeleteLearningResource),
                    Description = "Delete the learning resource",
                    Category = PermissionCategory.Subject
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of roles",
                    Code = nameof(PermissionConstants.Role.ViewRolesList),
                    Description = "View list of roles",
                    Category = PermissionCategory.Role
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of role",
                    Code = nameof(PermissionConstants.Role.ViewDetailOfRole),
                    Description = "View detail of role",
                    Category = PermissionCategory.Role
                },
                new Permission
                {
                    Id = i++,
                    Name = "Create role",
                    Code = nameof(PermissionConstants.Role.CreateRole),
                    Description = "Create role",
                    Category = PermissionCategory.Role
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update role",
                    Code = nameof(PermissionConstants.Role.UpdateRole),
                    Description = "Update role",
                    Category = PermissionCategory.Role
                },
                new Permission
                {
                    Id = i++,
                    Name = "Delete role",
                    Code = nameof(PermissionConstants.Role.DeleteRole),
                    Description = "Delete role",
                    Category = PermissionCategory.Role
                },
                new Permission
                {
                    Id = i++,
                    Name = "View list of users",
                    Code = nameof(PermissionConstants.Account.ViewUsersList),
                    Description = "View list of users",
                    Category = PermissionCategory.Account
                },
                new Permission
                {
                    Id = i++,
                    Name = "View detail of user",
                    Code = nameof(PermissionConstants.Account.ViewDetailOfUser),
                    Description = "View detail of user",
                    Category = PermissionCategory.Account
                },
                new Permission
                {
                    Id = i++,
                    Name = "Assign role to user",
                    Code = nameof(PermissionConstants.Account.AssignRoleToUser),
                    Description = "Assign role to user",
                    Category = PermissionCategory.Account
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update status",
                    Code = nameof(PermissionConstants.Account.UpdateStatus),
                    Description = "Update status of user account",
                    Category = PermissionCategory.Account
                },
                new Permission
                {
                    Id = i++,
                    Name = "View progress chart for all courses",
                    Code = nameof(PermissionConstants.Dashboard.ViewAllCoursesProgressChart),
                    Description = "View progress chart for all courses",
                    Category = PermissionCategory.Dashboard
                },
                new Permission
                {
                    Id = i++,
                    Name = "View progress chart for assigned courses",
                    Code = nameof(PermissionConstants.Dashboard.ViewAssignedCoursesProgressChart),
                    Description = "View progress chart for assigned courses",
                    Category = PermissionCategory.Dashboard
                },
                new Permission
                {
                    Id = i++,
                    Name = "View attendees learning progress chart in all courses",
                    Code = nameof(PermissionConstants.Dashboard.ViewAttendeesLearningProgressChartInAllCourses),
                    Description = "View attendees learning progress chart in all courses",
                    Category = PermissionCategory.Dashboard
                },
                new Permission
                {
                    Id = i++,
                    Name = "View attendees learning progress chart in assigned courses",
                    Code = nameof(PermissionConstants.Dashboard.ViewAttendeesLearningProgressChartInAssignedCourses),
                    Description = "View attendees learning progress chart in assigned courses",
                    Category = PermissionCategory.Dashboard
                },
                new Permission
                {
                    Id = i++,
                    Name = "View own learning progress chart in assigned courses",
                    Code = nameof(PermissionConstants.Dashboard.ViewOwnLearningProgressChartInAssignedCourses),
                    Description = "View own learning progress chart in assigned courses",
                    Category = PermissionCategory.Dashboard
                },
                new Permission
                {
                    Id = i++,
                    Name = "View user and role analytics",
                    Code = nameof(PermissionConstants.Dashboard.ViewUserRoleAnalytics),
                    Description = "View user and role analytics",
                    Category = PermissionCategory.Dashboard
                },
                new Permission
                {
                    Id = i++,
                    Name = "Update subject",
                    Code = nameof(PermissionConstants.Subject.UpdateSubject),
                    Description = "Update subject description",
                    Category = PermissionCategory.Subject
                },
            };
            return seedingList;
        }

        public static List<Role> SeedRoles()
        {
            int i = 1;
            List<Role> seedingList = new()
            {
                new Role
                {
                    Id = i++,
                    Name = "Authenticated User",
                    Description = "All logged in users.",
                    IsActive = true,
                    CreateTime = DateTimeOffset.Now
                },
                new Role
                {
                    Id = i++,
                    Name = "Admin",
                    Description = "System Administrator",
                    IsActive = true,
                    CreateTime = DateTimeOffset.Now
                },
                new Role
                {
                    Id = i++,
                    Name = "Student",
                    Description = "Users that take the course for learning.",
                    IsActive = true,
                    CreateTime = DateTimeOffset.Now
                },
                new Role
                {
                    Id = i++,
                    Name = "Teacher",
                    Description = "Users that take the course for teaching by manage resources, activites,...",
                    IsActive = true,
                    CreateTime = DateTimeOffset.Now
                },
                new Role
                {
                    Id = i++,
                    Name = "Manager",
                    Description = "Users that manage and monitor for learning.",
                    IsActive = true,
                    CreateTime = DateTimeOffset.Now
                }
            };
            return seedingList;
        }

        public static List<PermissionRole> SeedPermissionRoles()
        {
            List<Permission> permissions = SeedPermissions();
            List<Role> roles = SeedRoles();
            int i = 1;
            List<PermissionRole> seedingList = new()
            {
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.BasePermission.ViewProfile)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Authenticated User")).Id
                },
                 new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.BasePermission.EditProfile)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Authenticated User")).Id
                },
                  new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.BasePermission.ReceiveNotification)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Authenticated User")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewUserRoleAnalytics)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewAllCoursesProgressChart)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Role.ViewRolesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Role.ViewDetailOfRole)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Role.CreateRole)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Role.UpdateRole)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Role.DeleteRole)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Account.ViewUsersList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Account.ViewDetailOfUser)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Account.AssignRoleToUser)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Account.UpdateStatus)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewAllCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfCourse)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewListOfSurveyResultsInAllCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewSummaryOfSurveyResults)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Admin")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewOwnLearningProgressChartInAssignedCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewAssignedCoursesProgressChart)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewAssignedCoursesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfCourse)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewContentOfLearningResources)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.AttemptAndReattemptQuiz)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ReviewYourOwnAttempts)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.DoAndEditSurvey)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewOwnMarkReport)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewCoursesGrades)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfSurveyResult)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Student")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewAssignedCoursesProgressChart)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewAttendeesLearningProgressChartInAssignedCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewAssignedCoursesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfCourse)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.UpdateCourse)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.CreateTopic)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.UpdateTopic)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.DeleteTopic)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.AddLearningResource)
                        && p.Category == PermissionCategory.Course).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.UpdateLearningResource)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.DeleteLearningResource)
                        && p.Category == PermissionCategory.Course).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewContentOfLearningResources)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewAttendeesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewSummaryOfQuizResults)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfQuizResultOfStudent)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewListOfSurveyResultsInAssignedCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewSummaryOfSurveyResults)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewSummaryOfLearningProcessOfStudent)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfLearningProcessOfStudent)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.CreateQuiz)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.UpdateQuiz)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.DeleteQuiz)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.PreviewQuiz)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewStudentMarkReport)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewAssignedSubjectsList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewDetailOfSubject)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewLearningResourcesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewContentOfLearningResourcesInSubject)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.QuestionBank.CreateQuestionBank)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.QuestionBank.UpdateQuestionBank)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.QuestionBank.DeleteQuestionBank)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Question.ViewQuestionsList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Question.ViewDetailOfQuestion)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Question.CreateQuestion)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Question.UpdateQuestion)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Question.DeleteQuestion)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Teacher")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewAllCoursesProgressChart)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Dashboard.ViewAttendeesLearningProgressChartInAllCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewAllCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfCourse)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.CreateTopic)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.UpdateTopic)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.DeleteTopic)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewContentOfLearningResources)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewAttendeesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewSummaryOfQuizResults)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfQuizResultOfStudent)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewListOfSurveyResultsInAllCourses)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewSummaryOfSurveyResults)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewSummaryOfLearningProcessOfStudent)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewDetailOfLearningProcessOfStudent)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.PreviewQuiz)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.AddSurveyFromTemplate)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.UpdateSurvey)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.DeleteSurvey)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.PreviewSurvey)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Course.ViewStudentMarkReport)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.SurveyTemplate.CreateSurveyTemplate)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.SurveyTemplate.UpdateSurveyTemplate)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.SurveyTemplate.DeleteSurveyTemplate)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.SurveyTemplate.ViewDetailOfSurveyTemplate)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.SurveyTemplate.ViewSurveyTemplatesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewSubjectsList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewDetailOfSubject)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.CreateSection)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.UpdateSection)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.DeleteSection)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewLearningResourcesList)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.ViewContentOfLearningResourcesInSubject)).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.AddLearningResource)
                        && p.Category == PermissionCategory.Subject).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.DeleteLearningResource)
                        && p.Category == PermissionCategory.Subject).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                },
                new PermissionRole
                {
                    Id = i++,
                    PermissionId = permissions.Single(p =>
                        p.Code == nameof(PermissionConstants.Subject.UpdateSubject)
                        && p.Category == PermissionCategory.Subject).Id,
                    RoleId = roles.Single(r => r.Name.Equals("Manager")).Id
                }
            };
            return seedingList;
        }
    }
}
