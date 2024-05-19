using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_Users : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string UPassword { get; set; } = string.Empty;
        public string SecurityCode { get; set; } = string.Empty;
        public DateTime? OtpSendTime { get; set; }
        public DateTime DateOfJoin { get; set; }
        public int RoleId { get; set; }
        public int ParentId { get; set; }
        public bool IsPresent { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public string ProfileImage { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string? LastLoginIp { get; set; }
    }
}
