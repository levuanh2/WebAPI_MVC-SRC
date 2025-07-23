namespace TuNhua.Model
{
    public class RegisterVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; } // mặc định là Customer = 2
    }
    public class LoginVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserInfoVM
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string VaiTro { get; set; }
    }

}
