﻿namespace ViewModels.ViewModels.ResponseModel
{
    public class CurrentUserModel
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
