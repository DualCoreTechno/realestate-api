using Data.Entity;
using Data.Services;
using ViewModels.Enums;

namespace Data.Seed
{
    public static class DefaultRolePermissions
    {
        public static List<Tbl_Role_Permission> RolePermissionList()
        {
            List<Tbl_Role_Permission> permissions = new();

            AppMenu[] allMenus = (AppMenu[])Enum.GetValues(typeof(AppMenu));

            int id = 1;

            foreach (AppMenu menu in allMenus)
            {
                var singleRecord = new Tbl_Role_Permission
                {
                    Id = id,
                    MenuId = (int)menu,
                    RoleId = 1,
                    CreatedOn = DateConverterForData.GetSystemDateTime()
                };

                permissions.Add(singleRecord);

                id++;
            }

            return permissions;
        }
    }
}
