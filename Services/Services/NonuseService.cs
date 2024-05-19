using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class NonuseService : INonuseService
    {
        private readonly AppDB _dbContext;
        
        public NonuseService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllNonuse()
        {
            try
            {
                var result = await _dbContext.Tbl_Nonuse.Select(record => new MasterCommonFieldsModel
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

        public async Task<ResponseBaseModel> AddOrUpdateNonuse(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_Nonuse> checkValidation = _dbContext.Tbl_Nonuse.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_Nonuse> checkValidation = _dbContext.Tbl_Nonuse.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_Nonuse record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_Nonuse.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Nonuse updated successfully.");
                }

                _dbContext.Tbl_Nonuse.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Nonuse added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteNonuse(int id)
        {
            try
            {
                Tbl_Nonuse? record = await _dbContext.Tbl_Nonuse.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                    _dbContext.Tbl_Nonuse.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Nonuse deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the nonuse. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
