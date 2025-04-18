namespace UserEngagement.Infrastructure;

public static class Db
{
    public static class UserEngagement
    {
        public const string SCHEMA = "userengagement";
        public const string CONNECTION_STRING_NAME = "UserEngagementDbString";

        public static class Tables
        {
            public const string MESSAGES = "Messages";
            public const string MESSAGE_STATUSES = "MessageStatuses";
        }
    }

    public static class JobManagement
    {
        internal const string SCHEMA = "jobmanagement";
        internal const string CONNECTION_STRING_NAME = "JobManagementDbString";
    }
}