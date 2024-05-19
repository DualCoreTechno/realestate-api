namespace ViewModels.ViewModels
{
    public class ForgotPasswordModel
    {
        public int UserId { get; set; }
        public int Otp { get; set; }
        public string Password { get; set; } = String.Empty;
    }
}
