using BusinessLayer.Enum;

namespace TaskManager.Helpers
{
    public static class StatusHelper
    {
        public static Status GetStatusByCode(int statusInNumber)
        {
            switch (statusInNumber)
            {
                case 0:
                    return Status.Done;
                case 1:
                    return Status.Active;
                case 2:
                    return Status.Overdue;
                default:
                    return Status.Active;
            }
        }
    }
}
