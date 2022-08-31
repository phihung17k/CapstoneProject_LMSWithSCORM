namespace LMS.Core.Enum
{
    public enum LearningStatus
    {
        Undefined,
        InProgress,
        Passed,
        Failed
    }

    public enum LearningStatusWithoutUndefined
    {
        InProgress = LearningStatus.InProgress,
        Passed = LearningStatus.Passed,
        Failed = LearningStatus.Failed
    }
}
