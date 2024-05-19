using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class BuildingsService : IBuildingService
    {
        private readonly AppDB _dbContext;
        
        public BuildingsService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllBuildings()
        {
            try
            {
                var result = await _dbContext.Tbl_Buildings.Select(record => new MasterCommonFieldsModel
                {
                    Id = record.Id,
                    IsActive = record.IsActive,
                    Name = record.Name
                }).OrderBy(x => x.Name).ToListAsync();

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
        
        public async Task<ResponseBaseModel> AddOrUpdateBuilding(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_Buildings> checkValidation = _dbContext.Tbl_Buildings.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_Buildings> checkValidation = _dbContext.Tbl_Buildings.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_Buildings building = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    building.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_Buildings.Update(building);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Building updated successfully.");
                }

                _dbContext.Tbl_Buildings.Add(building);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Building updated successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
        
        public async Task<ResponseBaseModel> DeleteBuilding(int id)
        {
            try
            {
                Tbl_Buildings? building = await _dbContext.Tbl_Buildings.FirstOrDefaultAsync(x => x.Id == id);
                
                if (building != null)
                    _dbContext.Tbl_Buildings.Remove(building);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Building deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the building. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
