using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class RolesService : IRolesService
    {
        private readonly AppDB _dbContext;
        
        public RolesService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllRoles()
        {
            try
            {
                var result = await _dbContext.Tbl_Role.Select(record => new MasterCommonFieldsModel
                {
                    Id = record.Id,
                    IsActive = record.IsActive,
                    Name = record.Name
                }).Where(x => x.Name != RoleEnum.Admin.ToString()).OrderBy(x => x.Name).ToListAsync();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result);
                else
                    return new ResponseBaseModel(404, new List<MasterCommonFieldsModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdateRole(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_Role> checkValidation = _dbContext.Tbl_Role.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Role is already exists.");
                }
                else
                {
                    var roleName = await _dbContext.Tbl_Role.Where(x => x.Id == request.Id).Select(x => x.Name).FirstOrDefaultAsync();

                    if (roleName == RoleEnum.Admin.ToString())
                    {
                        return new ResponseBaseModel(500, "You can't update the details of admin user.");
                    }

                    IQueryable<Tbl_Role> checkValidation = _dbContext.Tbl_Role.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Role is already exists with another record.");
                }

                Tbl_Role record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_Role.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Role updated successfully.");
                }

                _dbContext.Tbl_Role.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Role added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteRole(int id)
        {
            try
            {
                Tbl_Role? record = await _dbContext.Tbl_Role.FirstOrDefaultAsync(x => x.Id == id);
                List<Tbl_Role_Permission>? permissionData = await _dbContext.Tbl_Role_Permission.Where(x => x.RoleId == id).ToListAsync();
                Tbl_Users? userData = await _dbContext.Tbl_Users.FirstOrDefaultAsync(x => x.RoleId == id);

                if (record != null)
                {
                    if(record.Name == RoleEnum.Admin.ToString())
                    {
                        return new ResponseBaseModel(500, "You can't delete admin role.");
                    }
                    else if (userData != null)
                    {
                        return new ResponseBaseModel(500, "You can't delete this role, because of it's assigned to some users");
                    }

                    if (permissionData != null && permissionData.Count > 0)
                        _dbContext.Tbl_Role_Permission.RemoveRange(permissionData);

                    _dbContext.Tbl_Role.Remove(record);
                }
                else
                {
                    return new ResponseBaseModel(404, "Record not found.");
                }

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Role deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the role. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
