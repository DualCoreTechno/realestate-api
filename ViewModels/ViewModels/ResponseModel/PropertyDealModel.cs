namespace ViewModels.ViewModels
{
    public class PropertyDealModel
    {
        public int Id { get; set; }
        public int DealTypeId { get; set; }
        public int UserId { get; set; }
        public string EmployeeName { get; set; } = String.Empty;
        public string? DealDate { get; set; }
        public string? PossessionDate { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string FlatOfficeNo { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerContactNo { get; set; } = string.Empty;
        public int PropertySourceId { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string BuyerContactNo { get; set; } = string.Empty;
        public int BuyerSourceId { get; set; }
        public int BhkOfficeId { get; set; }
        public decimal SquareFeet { get; set; }
        public decimal DealAmount { get; set; }
        public string OwnerBrokerage { get; set; } = string.Empty;
        public string ClientBrokerage { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public List<PropertyDealPaymentModel>? Payments { get; set; }
    }

    public class PropertyDealPaymentModel
    {
        public int PaymentOption { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; } = String.Empty;
        public string Remark { get; set; } = String.Empty;
    }

    public class PropertyDealPageLoadModel
    {
        public List<DropDownCommonResponse> PropertyTypeList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> UserList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> SourceList { get; set; } = new List<DropDownCommonResponse>();
        public List<DropDownCommonResponse> BhkOfficeList { get; set; } = new List<DropDownCommonResponse>();
    }
}