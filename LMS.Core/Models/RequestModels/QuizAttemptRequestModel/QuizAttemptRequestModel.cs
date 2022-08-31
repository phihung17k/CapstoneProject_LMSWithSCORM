using LMS.Core.Entity;
using System;

namespace LMS.Core.Models.RequestModels.QuizAttemptRequestModel
{
    public class QuizAttemptRequestModel
    {
        public int QuizId { get; set; }
    }

    public class QuizAttemptCreateModel
    {
        public long UserQuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
