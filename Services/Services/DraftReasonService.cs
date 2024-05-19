using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class DraftReasonService : IDraftReasonService
    {
        private readonly AppDB _dbContext;

        public DraftReasonService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllDraftReasons()
        {
            try
            {
                var result = await _dbContext.Tbl_DraftReason.Select(record => new MasterCommonFieldsModel
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

        public async Task<ResponseBaseModel> AddOrUpdateDraftReason(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_DraftReason> checkValidation = _dbContext.Tbl_DraftReason.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_DraftReason> checkValidation = _dbContext.Tbl_DraftReason.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_DraftReason reason = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    reason.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_DraftReason.Update(reason);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Draft reason updated successfully.");
                }

                _dbContext.Tbl_DraftReason.Add(reason);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Draft reason added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteDraftReason(int id)
        {
            try
            {
                Tbl_DraftReason? reason = await _dbContext.Tbl_DraftReason.FirstOrDefaultAsync(x => x.Id == id);

                if (reason != null)
                    _dbContext.Tbl_DraftReason.Remove(reason);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Draft reason deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the draft reason. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
