using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entity
{
    public class Tbl_Property : BaseEntity
    {
        public int UserId { get; set; }
        public int PropertyFor { get; set; }
        public string Address { get; set; } = String.Empty;
        public string Block { get; set; } = String.Empty;
        public string FlatNumber { get; set; } = String.Empty;
        public decimal? SuperBuiltupArea { get; set; }
        public decimal? CarpetArea { get; set; }
        public decimal? BuiltupArea { get; set; }
        public int? FurnitureStatusId { get; set; }
        public string Parking { get; set; } = String.Empty;
        public string? KeyStatus { get; set; }
        public decimal? PropertyPrice { get; set; }
        public string OwnerName { get; set; } = String.Empty;
        public string Mobile { get; set; } = String.Empty;
        public string Mobile1 { get; set; } = String.Empty;
        public string Mobile2 { get; set; } = String.Empty;
        public string Comission { get; set; } = String.Empty;
        public string Remark { get; set; } = String.Empty;
        public DateTime? AvailableFrom { get; set; }


        [ForeignKey("BhkOfficeId")]
        public int? BhkOfficeId { get; set; }

        [ForeignKey("BuildingId")]
        public int? BuildingId { get; set; }

        [ForeignKey("AreaId")]
        public int? AreaId { get; set; }

        [ForeignKey("MeasurementId")]
        public int? MeasurementId { get; set; }

        [ForeignKey("SourceId")]
        public int? SourceId { get; set; }

        [ForeignKey("PropertyStatusId")]
        public int? PropertyStatusId { get; set; }

        public virtual Tbl_BhkOffice? BhkOffice { get; set; }
        public virtual Tbl_Buildings? Building { get; set; }
        public virtual Tbl_Area? Area { get; set; }
        public virtual Tbl_Measurement? Measurement { get; set; }
        public virtual Tbl_Source? Source { get; set; }
        public virtual Tbl_PropertyStatus? PropertyStatus { get; set; }
    }
}