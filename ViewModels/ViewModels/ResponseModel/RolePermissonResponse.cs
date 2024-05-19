namespace ViewModels.ViewModels
{
    public class RolePermissonResponse
    {
        public int RoleId { get; set; }
        public List<int> MenuIds { get; set; } = new List<int>();
    }
}
