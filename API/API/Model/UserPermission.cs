namespace API.Model
{
    public class UserPermission
    {
        public int Id { get; set; }
        public UserTenant? UserTenant { get; set; }
        public Feature? Feature { get; set; }
        public Operation? Action { get; set; }
    }
}
