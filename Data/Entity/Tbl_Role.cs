namespace Data.Entity
{
    public class Tbl_Role: BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;


        public virtual List<Tbl_Role_Permission>? TblRolePermission{ get; set; }
    }
}
