using LMS.Core.Enum;
using System;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IUserCourseService
    {
        Task CheckCourseAccessibility(int courseId, Guid userId, ActionMethods methods, bool isStudent = false, 
            bool isTeacher = false, bool isManager = false);

        Task CheckTopicAccessibility(int topicId, Guid userId, ActionMethods methods, bool isStudent = false, 
            bool isTeacher = false, bool isManager = false);

        Task CheckTopicResourceAccessibility(int topicResourceId, Guid userId, TopicResourceType resourceType,
            ActionMethods methods, bool isStudent = false, bool isTeacher = false, bool isManager = false);

        Task CheckViewingCourseDetail(int courseId, Guid userId);
        Task CheckViewingListAttendessInCourse(int courseId, Guid userId);
    }
}
