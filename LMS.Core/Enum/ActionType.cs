namespace LMS.Core.Enum
{
    public enum ActionType
    {
        Study,
        Teach,
        Manage
    }

    public enum ActionTypeWithoutStudy
    {
        Teach = ActionType.Teach,
        Manage = ActionType.Manage,
    }
}
