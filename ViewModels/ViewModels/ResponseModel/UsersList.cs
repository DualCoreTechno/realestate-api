namespace ViewModels.ViewModels
{
    public class UserList
    {
        public string Id { get; set; } = String.Empty;
        public int UserId { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = String.Empty;
        public int ParentId { get; set; }
        public string ParentName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Mobile { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string ProfileImage { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public string DateOfJoin { get; set; } = String.Empty;
    }

    public class UserProfile
    {
        public string UserName {  get; set; } = string.Empty;
        public int[] MenuIds { get; set; } = Array.Empty<int>();
        public string ProfileImage { get; set; } = string.Empty;
    }

    public class UserPageLoadModel
    {
        public List<DropDownCommonResponse> RoleList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> UserList { get; set; } = new List<DropDownCommonResponse>();
    }
}
