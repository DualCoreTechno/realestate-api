﻿namespace Data.Entity
{
    public class Tbl_Measurement : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;

        public virtual List<Tbl_Property>? Property { get; set; }
    }
}
