namespace API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizePermissionAttribute : Attribute
    {
        public string Action { get; }
        public string Feature { get; }

        public AuthorizePermissionAttribute(string feature, string permission)
        {
            Feature = feature;
            Action = permission;
        }
    }
}
