namespace Data.Entity
{
    public class Tbl_Source : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;


        public virtual List<Tbl_Property>? Property { get; set; }
       
        //public virtual List<Tbl_PropertyDeal>? PropertyDealProperty { get; set; }
        //public virtual List<Tbl_PropertyDeal>? PropertyDealBuyer { get; set; }
        public virtual List<Tbl_Enquiry>? Enquiry { get; set; }
    }
}
