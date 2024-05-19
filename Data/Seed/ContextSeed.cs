using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CreateDefaultRecords(modelBuilder);
        }

        private static void CreateDefaultRecords(ModelBuilder modelBuilder)
        {
            // Add roles
            modelBuilder.Entity<Tbl_Role>().HasData(DefaultRoles.RoleList());

            // Add role permissions
            modelBuilder.Entity<Tbl_Role_Permission>().HasData(DefaultRolePermissions.RolePermissionList());

            // Add default enquiry status
            //modelBuilder.Entity<Tbl_EnquiryStatus>().HasData(DefaultEnquiryStatus.EnquiryStatusList());
        }
    }
}