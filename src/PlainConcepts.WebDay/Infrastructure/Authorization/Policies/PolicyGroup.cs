namespace PlainConcepts.WebDay.Infrastructure.Authorization.Policies
{
    public class PolicyGroup
    {
        public static string[] CreateUserGroupPolicy = {
            Policy.Admin,
            Policy.Writer
        };

        public static string[] ListUsersGroupPolicy =
        {
            Policy.Admin,
            Policy.Reader,
            Policy.Writer
        };

        public static string[] RemoveUserGroupPolicy =
        {
            Policy.Admin
        };
    }
}
