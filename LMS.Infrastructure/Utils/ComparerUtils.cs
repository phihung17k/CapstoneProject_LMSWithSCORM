using LMS.Core.Entity;
using LMS.Core.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LMS.Infrastructure.Utils
{
    public class OptionComparer : IEqualityComparer<Option>
    {
        public bool Equals(Option x, Option y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Option obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class TemplateOptionComparer : IEqualityComparer<TemplateOption>
    {
        public bool Equals(TemplateOption x, TemplateOption y)
        {
            return x.Content == y.Content;
        }

        public int GetHashCode([DisallowNull] TemplateOption obj)
        {
            return obj.Content.GetHashCode();
        }
    }

    public class SurveyOptionComparer : IEqualityComparer<SurveyOption>
    {
        public bool Equals(SurveyOption x, SurveyOption y)
        {
            return x.Content == y.Content;
        }

        public int GetHashCode([DisallowNull] SurveyOption obj)
        {
            return obj.Content.GetHashCode();
        }
    }

    public class UserCourseComparer : IEqualityComparer<UserCourse>
    {
        public bool Equals(UserCourse x, UserCourse y)
        {
            return x.UserId == y.UserId && x.CourseId == y.CourseId;
        }

        public int GetHashCode([DisallowNull] UserCourse obj)
        {
            return (obj.UserId.ToString() + obj.CourseId.ToString()).GetHashCode();
        }
    }

    public class CourseInformationModelComparer : IEqualityComparer<CourseInformationModel>
    {
        public bool Equals(CourseInformationModel x, CourseInformationModel y)
        {
            return x.CourseId == y.CourseId;
        }

        public int GetHashCode([DisallowNull] CourseInformationModel obj)
        {
            return obj.CourseId.GetHashCode();
        }
    }

    public class UserSubjectComparer : IEqualityComparer<UserSubject>
    {
        public bool Equals(UserSubject x, UserSubject y)
        {
            return x.UserId == y.UserId && x.SubjectId == y.SubjectId;
        }

        public int GetHashCode([DisallowNull] UserSubject obj)
        {
            return (obj.UserId.ToString() + obj.SubjectId.ToString()).GetHashCode();
        }
    }
}
