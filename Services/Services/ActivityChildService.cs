using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class ActivityChildService : IActivityChildService
    {
        private readonly AppDB _dbContext;
        
        public ActivityChildService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetPageLoadData()
        {
            try
            {
                var activityParent = await _dbContext.Tbl_ActivityParent.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive
                }).OrderBy(x => x.Text).ToListAsync();

                ActivityChildResponseModel response = new()
                {
                    ActivityParentList = activityParent
                };

                return new ResponseBaseModel(200, response);
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllActivityChild()
        {
            try
            {
                IQueryable<ActivityChildModel> query =
                (
                    from ActivityChild in _dbContext.Tbl_ActivityChild
                    
                    join ActivityParent in _dbContext.Tbl_ActivityParent on ActivityChild.ActivityParentId equals ActivityParent.Id into ActivityParentTemp
                    from ActivityParent in ActivityParentTemp.DefaultIfEmpty()

                    orderby ActivityChild.Name ascending

                    select new ActivityChildModel
                    {
                        Id = ActivityChild.Id,
                        IsActive = ActivityChild.IsActive,
                        Name = ActivityChild.Name,
                        ActivityParentId = ActivityChild.ActivityParentId,
                        ActivityParentName = ActivityParent.Name
                    }
                ).AsQueryable();

                int totalRecords = query.Count();

                List<ActivityChildModel> result = await query.ToListAsync();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result, totalRecords);
                else
                    return new ResponseBaseModel(404, new List<ActivityChildModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdateActivityChild(ActivityChildModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_ActivityChild> checkValidation = _dbContext.Tbl_ActivityChild.Where(x => x.Name == request.Name);

                    if(checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_ActivityChild> checkValidation = _dbContext.Tbl_ActivityChild.Where(x => x.Id != request.Id && x.Name == request.Name);
                    
                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_ActivityChild activity = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    ActivityParentId = request.ActivityParentId
                };

                if (request.Id != null && request.Id != 0)
                {
                    activity.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_ActivityChild.Update(activity);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Child activity updated successfully.");
                }

                _dbContext.Tbl_ActivityChild.Add(activity);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Child activity added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
        
        public async Task<ResponseBaseModel> DeleteActivityChild(int id)
        {
            try
            {
                Tbl_ActivityChild? record = await _dbContext.Tbl_ActivityChild.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                    _dbContext.Tbl_ActivityChild.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Child activity deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the child activity. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}