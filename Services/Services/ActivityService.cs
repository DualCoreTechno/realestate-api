using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDB _dbContext;

        public ActivityService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllActivity()
        {
            try
            {
                List<Tbl_Activity> records = await _dbContext.Tbl_Activity.ToListAsync();
                
                List<ActivityModel> result = records.Select(record => new ActivityModel
                {
                    Id = record.Id,
                    IsActive = record.IsActive,
                    Name = record.Name,
                    IsParentActivity = record.IsParent,
                    ParentActivityId = record.ParentActivityId
                }).ToList();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result);
                else
                    return new ResponseBaseModel(404, new List<ActivityModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllParentActivity()
        {
            try
            {
                List<Tbl_Activity> records = await _dbContext.Tbl_Activity.Where(r => r.IsParent == true && r.ParentActivityId == null).ToListAsync();

                List<ActivityModel> result = records.Select(record => new ActivityModel
                {
                    Id = record.Id,
                    IsActive = record.IsActive,
                    Name = record.Name,
                    IsParentActivity = record.IsParent,
                }).ToList();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result);
                else
                    return new ResponseBaseModel(404, new List<ActivityModel>(), "No  record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdateActivity(ActivityModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_Activity> checkValidation = _dbContext.Tbl_Activity.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_Activity> checkValidation = _dbContext.Tbl_Activity.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_Activity record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    ParentActivityId = request.IsParentActivity == true ? null : request.ParentActivityId,
                    IsParent = request.IsParentActivity
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_Activity.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Activity updated successfully.");
                }

                _dbContext.Tbl_Activity.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Activity added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteActivity(int id)
        {
            try
            {
                int childCount = await _dbContext.Tbl_Activity.Where(x => x.ParentActivityId == id).CountAsync();

                if (childCount > 0)
                {
                    return new ResponseBaseModel(500, "This activity have one or more child activity, please remove child first.");
                }

                Tbl_Activity? record = await _dbContext.Tbl_Activity.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                    _dbContext.Tbl_Activity.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Activity deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the activity. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
