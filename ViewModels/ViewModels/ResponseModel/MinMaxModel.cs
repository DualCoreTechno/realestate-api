namespace ViewModels.ViewModels
{
    public class MinMaxModel
    {
        public int Id { get; set; }
        public int For { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string MinTitle { get; set; } = string.Empty;
        public string MaxTitle { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}