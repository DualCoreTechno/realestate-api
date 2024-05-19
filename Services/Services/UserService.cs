using Data.Data;
using Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Settings;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace Services.Services
{

    public class UserService : IUserService
    {
        private readonly AppDB _dbContext;
        private readonly UserManager<Tbl_Users> _userManager;
        private readonly IUserChannelService _userChannelService;

        public UserService(AppDB dbContext, UserManager<Tbl_Users> userManager, IUserChannelService userChannelService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _userChannelService = userChannelService;
        }

        public async Task<ResponseBaseModel> GetPageLoadDataAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    List<Tbl_Users> userList;

                    if (userRole == RoleEnum.Admin.ToString())
                    {
                        userList = await _dbContext.Tbl_Users.OrderBy(x => x.FirstName).ToListAsync();
                    }
                    else
                    {
                        int[] userChannel = await _userChannelService.GetUserChannelAsync(userId);
                        userList = await _dbContext.Tbl_Users.Where(x => userChannel.Contains(x.UserId)).OrderBy(x => x.FirstName).ToListAsync();
                    }

                    var roleList = await _dbContext.Tbl_Role.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive
                    }).OrderBy(x => x.Text).ToListAsync();

                    UserPageLoadModel response = new()
                    {
                        RoleList = roleList,

                        UserList = userList.Select(record => new DropDownCommonResponse
                        {
                            Id = record.UserId,
                            Text = $"{record.FirstName} {record.LastName}",
                            IsActive = record.IsActive,

                        }).ToList()
                    };

                    return new ResponseBaseModel(200, response);
                }
                else
                {
                    return new ResponseBaseModel(404, new List<PropertyModel>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllUserListAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    int[]? userChannel = null;

                    if (userRole != RoleEnum.Admin.ToString())
                        userChannel = await _userChannelService.GetUserChannelAsync(userId);

                    IQueryable<UserList> query =
                    (
                        from Users in _dbContext.Tbl_Users
                        where (userChannel == null || userChannel.Contains(Users.UserId)) && Users.UserId != userId

                        join Parents in _dbContext.Tbl_Users on Users.ParentId equals Parents.UserId into ParentsTemp
                        from Parents in ParentsTemp.DefaultIfEmpty()

                        join Role in _dbContext.Tbl_Role on Users.RoleId equals Role.Id into RoleTemp
                        from Role in RoleTemp.DefaultIfEmpty()

                        orderby Users.CreatedOn descending

                        select new UserList
                        {
                            Id = Users.Id,
                            UserId = Users.UserId,
                            Email = Users.Email,
                            Mobile = Users.PhoneNumber,
                            FirstName = Users.FirstName,
                            LastName = Users.LastName,
                            IsActive = Users.IsActive,
                            RoleId = Users.RoleId,
                            RoleName = Role.Name,
                            ParentId = Users.ParentId,
                            ParentName = $"{Parents.FirstName} {Parents.LastName}",
                            Password = Users.UPassword,
                            DateOfJoin = DateConverter.ToStringDateTime(Users.DateOfJoin)
                        }
                    ).AsQueryable();

                    int totalRecords = query.Count();

                    List<UserList> result = await query.ToListAsync();

                    if (result.Count > 0)
                        return new ResponseBaseModel(200, result, totalRecords);
                    else
                        return new ResponseBaseModel(404, new List<UserList>(), "No record(s) found.");
                }
                else
                {
                    return new ResponseBaseModel(404, new List<UserList>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetUserDetailsWithImageAsync(int userId)
        {
            try
            {
                Tbl_Users? record = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (record != null)
                {
                    var roleName = await _dbContext.Tbl_Role.Where(x => x.Id == record.RoleId).Select(x => x.Name).FirstOrDefaultAsync();
                    var parentdetails = await _dbContext.Tbl_Users.Where(x => x.UserId == record.ParentId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefaultAsync();

                    var result = new UserList
                    {
                        Id = record.Id,
                        Email = record.Email,
                        Mobile = record.PhoneNumber,
                        FirstName = record.FirstName,
                        LastName = record.LastName,
                        IsActive = record.IsActive,
                        RoleId = record.RoleId,
                        RoleName = roleName ?? "",
                        ParentId = record.ParentId,
                        ParentName = parentdetails ?? "",
                        Password = record.UPassword,
                        DateOfJoin = DateConverter.ToStringDateTime(record.DateOfJoin),
                        ProfileImage = record.ProfileImage
                    };

                    return new ResponseBaseModel(200, result);
                }
                else
                {
                    return new ResponseBaseModel(404, new UserList(), "No record(s) found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetUserProfileAsync(string userName, int RoleId)
        {
            try
            {
                List<int> menuIdArray = await _dbContext.Tbl_Role_Permission.Where(x => x.RoleId == RoleId).Select(y => y.MenuId).ToListAsync();
                var userInfo = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.UserName == userName);

                var result = new UserProfile
                {
                    UserName = userName,
                    MenuIds = menuIdArray.ToArray(),
                    ProfileImage = userInfo == null ? "" : userInfo.ProfileImage
                };

                return new ResponseBaseModel(200, result);
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> CreateUserAsync(UserSignup userSignup)
        {
            try
            {
                if (userSignup.RoleId <= 0)
                {
                    return new ResponseBaseModel(500, "Role is required.");
                }

                var checkMobileExists = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.PhoneNumber == userSignup.PhoneNumber);

                if (checkMobileExists != null)
                {
                    return new ResponseBaseModel(500, "Mobile number already exists.");
                }

                var isUserNameAlreadyExist = await _dbContext.Tbl_Users.AnyAsync(x => x.FirstName.ToUpper() == userSignup.FirstName.ToUpper() && x.LastName.ToUpper() == userSignup.LastName.ToUpper());

                if (isUserNameAlreadyExist)
                {
                    return new ResponseBaseModel(500, "First name and last name already exists.");
                }

                var roleName = await _dbContext.Tbl_Role.Where(x => x.Id == userSignup.RoleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (roleName == RoleEnum.Admin.ToString())
                {
                    var checkAdminExists = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.RoleId == userSignup.RoleId);

                    if (checkAdminExists != null)
                    {
                        return new ResponseBaseModel(500, "Admin user already exists.");
                    }

                    userSignup.ParentId = 0;
                }
                else if (roleName != RoleEnum.Admin.ToString() && userSignup.ParentId <= 0)
                {
                    return new ResponseBaseModel(500, "Parent is required.");
                }

                Tbl_Users user = new()
                {
                    FirstName = userSignup.FirstName,
                    LastName = userSignup.LastName,
                    Email = userSignup.Email,
                    UserName = userSignup.FirstName + userSignup.LastName,
                    UPassword = userSignup.Password,
                    PhoneNumber = userSignup.PhoneNumber,
                    RoleId = userSignup.RoleId,
                    ParentId = userSignup.ParentId,
                    IsActive = userSignup.IsActive,
                    DateOfJoin = DateConverter.ToDateTimeWithoutNull(userSignup.DateOfJoin),
                    CreatedOn = DateConverter.GetSystemDateTime(),
                    ProfileImage = userSignup.ProfileImage
                };

                IdentityResult result = await _userManager.CreateAsync(user, userSignup.Password);

                if (result.Succeeded)
                {
                    return new ResponseBaseModel(200, "User created successfully.");
                }

                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> UpdateUserAsync(UserSignup userSignup)
        {
            try
            {
                var isUserNameAlreadyExist = await _dbContext.Tbl_Users.AnyAsync(x => x.FirstName.ToUpper() == userSignup.FirstName.ToUpper() &&
                            x.LastName.ToUpper() == userSignup.LastName.ToUpper() && x.Id != userSignup.Id);

                if (isUserNameAlreadyExist)
                {
                    return new ResponseBaseModel(500, "First name and last name already exists.");
                }

                var userInfo = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.Id == userSignup.Id);

                if (userInfo != null)
                {
                    userInfo.FirstName = userSignup.FirstName;
                    userInfo.LastName = userSignup.LastName;
                    userInfo.RoleId = userSignup.RoleId;
                    userInfo.ParentId = userSignup.ParentId;
                    userInfo.IsPresent = true;
                    userInfo.IsActive = userSignup.IsActive;
                    userInfo.DateOfJoin = DateConverter.ToDateTimeWithoutNull(userSignup.DateOfJoin);
                    userInfo.UpdatedOn = DateConverter.GetSystemDateTime();

                    if (!string.IsNullOrEmpty(userSignup.ProfileImage))
                        userInfo.ProfileImage = userSignup.ProfileImage;

                    _dbContext.Tbl_Users.Update(userInfo);
                    _dbContext.Entry(userInfo).Property(x => x.UserId).IsModified = false;
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "User updated successfully.");
                }

                return new ResponseBaseModel(404, "Record not found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> UpdateMyProfileAsync(UserSignup userSignup, int userId)
        {
            try
            {
                var userInfo = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (userInfo != null)
                {
                    userInfo.FirstName = userSignup.FirstName;
                    userInfo.LastName = userSignup.LastName;

                    if (!string.IsNullOrEmpty(userSignup.ProfileImage))
                        userInfo.ProfileImage = userSignup.ProfileImage;

                    _dbContext.Tbl_Users.Update(userInfo);
                    _dbContext.Entry(userInfo).Property(x => x.UserId).IsModified = false;
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Profile updated successfully.");
                }

                return new ResponseBaseModel(404, "Record not found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteUserAsync(string userId)
        {
            try
            {
                Tbl_Users? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user != null)
                    await _userManager.DeleteAsync(user);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "User deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the user. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            try
            {
                Tbl_Users? user = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.UserId == changePassword.UserId);

                if (user != null)
                {
                    bool result = await _userManager.CheckPasswordAsync(user, changePassword.OldPassword);

                    if (!result)
                        return new ResponseBaseModel(500, "Old password is wrong.");

                    user.UPassword = changePassword.NewPassword;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePassword.NewPassword);

                    _dbContext.Tbl_Users.Update(user);
                    _dbContext.Entry(user).Property(x => x.UserId).IsModified = false;
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Password changed successfully.");
                }
                else
                {
                    return new ResponseBaseModel(404, "User not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> ResetPasswordAsync(ChangePasswordModel changePassword)
        {
            try
            {
                Tbl_Users? user = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.UserId == changePassword.UserId);

                if (user != null)
                {
                    user.UPassword = changePassword.NewPassword;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePassword.NewPassword);

                    _dbContext.Tbl_Users.Update(user);
                    _dbContext.Entry(user).Property(x => x.UserId).IsModified = false;
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Password reset successfully.");
                }
                else
                {
                    return new ResponseBaseModel(404, "User not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
