namespace ViewModels.ViewModels
{
    public class UserSignup
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public int RoleId { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; } = true;
        public string DateOfJoin { get; set; } = String.Empty;
        public string ProfileImage { get; set; } = String.Empty;
    }
}
