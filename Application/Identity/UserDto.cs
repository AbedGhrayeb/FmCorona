namespace Application.Identity
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return this.FirstName + " " + this.LastName; } }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
