namespace ViewModels.ViewModels
{
    public class MasterCommonFieldsModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; }
    }
    
    public class DropDownCommonResponse
    {
        public int? Id { get; set; }
        public string Text { get; set; } = String.Empty;
        public bool IsActive { get; set; } 
    }
}
