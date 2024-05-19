using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly AppDB _dbContext;
        
        public BudgetService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetPageLoadData()
        {
            try
            {
                var bhkOfficeList = await _dbContext.Tbl_BhkOffice.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive
                }).OrderBy(x => x.Text).ToListAsync();

                BudgetResponseModel response = new()
                {
                    BhkOfficeList = bhkOfficeList
                };

                return new ResponseBaseModel(200, response);
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllBudget()
        {
            try
            {
                IQueryable<BudgetModel> query =
                (
                    from Budget in _dbContext.Tbl_Budget
                    
                    join BhkOffice in _dbContext.Tbl_BhkOffice on Budget.BhkOfficeId equals BhkOffice.Id into BhkOfficeTemp
                    from BhkOffice in BhkOfficeTemp.DefaultIfEmpty()
                    
                    orderby Budget.Name ascending

                    select new BudgetModel
                    {
                        Id = Budget.Id,
                        IsActive = Budget.IsActive,
                        Name = Budget.Name,
                        From = Budget.From,
                        To = Budget.To,
                        ForValue = Budget.For,
                        BhkOfficeId = Budget.BhkOfficeId,
                        BhkOfficeName = BhkOffice.Name
                    }
                ).AsQueryable();

                int totalRecords = query.Count();

                List<BudgetModel> result = await query.ToListAsync();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result, totalRecords);
                else
                    return new ResponseBaseModel(404, new List<BudgetModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
        
        public async Task<ResponseBaseModel> AddOrUpdateBudget(BudgetModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_Budget> checkValidation = _dbContext.Tbl_Budget.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_Budget> checkValidation = _dbContext.Tbl_Budget.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_Budget record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    BhkOfficeId = request.BhkOfficeId,
                    From = request.From,
                    For = request.ForValue,
                    To = request.To
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_Budget.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Budget updated successfully.");
                }

                _dbContext.Tbl_Budget.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Budget added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteBudget(int id)
        {
            try
            {
                Tbl_Budget? record = await _dbContext.Tbl_Budget.FirstOrDefaultAsync(x => x.Id == id);
                
                if (record != null)
                    _dbContext.Tbl_Budget.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Budget deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the budget. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
