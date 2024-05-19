using Data.Entity;
using Data.Services;
using ViewModels.Enums;

namespace Data.Seed
{
    public static class DefaultRoles
    {
        public static List<Tbl_Role> RoleList()
        {
            return new List<Tbl_Role>
            {
                new() 
                { 
                    Id = 1,
                    Name = RoleEnum.Admin.ToString(), 
                    IsActive = true,
                    CreatedOn = DateConverterForData.GetSystemDateTime()
                }
            };
        }
    }
}
