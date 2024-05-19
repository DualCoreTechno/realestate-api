using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class FurnitureStatusService : IFurnitureStatusService
    {
        private readonly AppDB _dbContext;

        public FurnitureStatusService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllFurnitureStatus()
        {
            try
            {
                var result = await _dbContext.Tbl_FurnitureStatus.Select(record => new MasterCommonFieldsModel
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

        public async Task<ResponseBaseModel> AddOrUpdateFurnitureStatus(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_FurnitureStatus> checkValidation = _dbContext.Tbl_FurnitureStatus.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_FurnitureStatus> checkValidation = _dbContext.Tbl_FurnitureStatus.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_FurnitureStatus record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_FurnitureStatus.Update(record);
                    await _dbContext.SaveChangesAsync();
                    
                    return new ResponseBaseModel(200, "Furniture status updated successfully.");
                }

                _dbContext.Tbl_FurnitureStatus.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Furniture status added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteFurnitureStatus(int id)
        {
            try
            {
                Tbl_FurnitureStatus? record = await _dbContext.Tbl_FurnitureStatus.FirstOrDefaultAsync(x => x.Id == id);
                if (record != null)
                    _dbContext.Tbl_FurnitureStatus.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Furniture status deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the furniture status. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
