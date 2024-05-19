namespace ViewModels.ViewModels
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = String.Empty;
        public string NewPassword { get; set; } = String.Empty;
    }
}
