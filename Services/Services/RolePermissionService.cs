using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.Common;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly AppDB _dbContext;
        private readonly ILogDataService _logDataService;

        public RolePermissionService(AppDB dbContext, ILogDataService logDataService)
        {
            _dbContext = dbContext;
            _logDataService = logDataService;
        }

        public async Task<ResponseBaseModel> GetRolePermission(int id)
        {
            try
            {
                List<int> records = await _dbContext.Tbl_Role_Permission.Where(x => x.RoleId == id).Select(y => y.MenuId).ToListAsync();

                RolePermissonResponse response = new()
                {
                    RoleId = id,
                    MenuIds = records
                };

                if (records.Count > 0)
                    return new ResponseBaseModel(200, response);
                else
                    return new ResponseBaseModel(404, new List<RolePermissonResponse>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> UpdateRolePermission(RolePermissonResponse request, int userId)
        {
            try
            {
                var roleName = await _dbContext.Tbl_Role.Where(x => x.Id == request.RoleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (roleName == RoleEnum.Admin.ToString())
                {
                    return new ResponseBaseModel(500, "You can't change the permissions of admin user.");
                }

                var values = await _dbContext.Tbl_Role_Permission.Where(x => x.RoleId == request.RoleId).ToListAsync();

                if (values.Count > 0)
                {
                    foreach (var item in values)
                    {
                        _dbContext.Tbl_Role_Permission.Remove(item);
                    }
                }

                var dataToSave = request.MenuIds.Select(menu => new Tbl_Role_Permission
                {
                    MenuId = menu,
                    RoleId = request.RoleId
                }).ToList();

                await _dbContext.Tbl_Role_Permission.AddRangeAsync(dataToSave);

                await _dbContext.SaveChangesAsync();

                var logs = new LogDataModel
                {
                    UserId = userId,
                    Module = LogTypeEnum.MasterLog,
                    PageName = TableNameArray.GetAliasForTable("Tbl_Role_Permission"),
                    IpAddress = "",
                    Description = $"Permission Updated For {roleName} Role"
                };

                await _logDataService.AddLogData(logs);

                return new ResponseBaseModel(200, "Role permission updated successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
