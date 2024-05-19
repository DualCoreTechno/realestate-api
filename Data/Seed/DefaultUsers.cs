using Data.Entity;
using Data.Services;
using Microsoft.AspNetCore.Identity;

namespace Data.Seed
{
    public static class DefaultUsers
    {
        public static async Task SeedDefaultUser(UserManager<Tbl_Users> userManager)
        {
            var checkUser = await userManager.FindByEmailAsync("admin@gmail.com");

            if (checkUser == null)
            {
                var user = new Tbl_Users
                {
                    // TODO: change value here
                    FirstName = "",
                    LastName = "",
                    Email = "",
                    UserName = "" + "",
                    UPassword = "Admin@123",
                    PhoneNumber = "",
                    RoleId = 1,
                    ParentId = 0,
                    IsActive = true,
                    DateOfJoin = DateConverterForData.GetSystemDateTime(),
                    ProfileImage = "",
                    CreatedOn = DateConverterForData.GetSystemDateTime()
                };

                var result = await userManager.CreateAsync(user, "Abc@123");
            }
        }
    }
}