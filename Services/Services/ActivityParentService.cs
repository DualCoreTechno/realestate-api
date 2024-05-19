using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class ActivityParentService : IActivityParentService
    {
        private readonly AppDB _dbContext;
        
        public ActivityParentService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllActivityParent()
        {
            try
            {
                var result = await _dbContext.Tbl_ActivityParent.Select(record => new MasterCommonFieldsModel
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

        public async Task<ResponseBaseModel> AddOrUpdateActivityParent(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_ActivityParent> checkValidation = _dbContext.Tbl_ActivityParent.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_ActivityParent> checkValidation = _dbContext.Tbl_ActivityParent.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_ActivityParent activity = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    activity.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_ActivityParent.Update(activity);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Parent activity updated successfully.");
                }

                _dbContext.Tbl_ActivityParent.Add(activity);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Parent activity added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
        
        public async Task<ResponseBaseModel> DeleteActivityParent(int id)
        {
            try
            {
                Tbl_ActivityParent? record = await _dbContext.Tbl_ActivityParent.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                    _dbContext.Tbl_ActivityParent.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Parent activity deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the Parent activity. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}