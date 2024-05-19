namespace Data.Entity
{
    public class Tbl_PropertyDealPayment : BaseEntity
    {
        public int PropertyDealId { get; set; }
        public int PaymentOption { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; } = String.Empty;
        public string Remark { get; set; } = String.Empty;
    }
}
