using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class MinMaxService : IMinMaxService
    {
        private readonly AppDB _dbContext;

        public MinMaxService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllMinMax()
        {
            try
            {
                var result = await _dbContext.Tbl_MinMax.Select(record => new MinMaxModel
                {
                    Id = record.Id,
                    For = record.For,
                    Min = record.MinValue,
                    Max = record.MaxValue,
                    MinTitle = record.MinTitle,
                    MaxTitle = record.MaxTitle,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.For).ToListAsync();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result);
                else
                    return new ResponseBaseModel(404, new List<MinMaxModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdateMinMax(MinMaxModel request)
        {
            try
            {
                Tbl_MinMax record = new()
                {
                    For = request.For,
                    MinValue = request.Min,
                    MaxValue = request.Max,
                    MinTitle = request.MinTitle,
                    MaxTitle = request.MaxTitle,
                    IsActive = request.IsActive
                };

                if (request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_MinMax.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Min-Max updated successfully.");
                }

                _dbContext.Tbl_MinMax.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Min-Max added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteMinMax(int id)
        {
            try
            {
                Tbl_MinMax? record = await _dbContext.Tbl_MinMax.SingleOrDefaultAsync(x => x.Id == id);

                if (record != null)
                    _dbContext.Tbl_MinMax.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Min-Max deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the min-max. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
