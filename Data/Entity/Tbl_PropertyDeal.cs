using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_PropertyDeal : BaseEntity
    {
        public int UserId { get; set; }
        public int DealTypeId { get; set; }
        public DateTime? DealDate { get; set; }
        public DateTime? PossessionDate { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string FlatOfficeNo { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerContactNo { get; set; } = string.Empty;
        public string BuyerName { get; set; } = string.Empty;
        public string BuyerContactNo { get; set; } = string.Empty;
        public decimal SquareFeet { get; set; }
        public decimal DealAmount { get; set; }
        public string OwnerBrokerage { get; set; } = string.Empty;
        public string ClientBrokerage { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;


        [ForeignKey("PropertySourceId")]
        public int PropertySourceId { get; set; }

        [ForeignKey("BuyerSourceId")]
        public int BuyerSourceId { get; set; }

        [ForeignKey("BhkOfficeId")]
        public int BhkOfficeId { get; set; }


        public virtual Tbl_Source? PropertySource { get; set; }
        public virtual Tbl_Source? BuyerSource { get; set; }
        public virtual Tbl_BhkOffice? BhkOffice { get; set; }
    }
}
