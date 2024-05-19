using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class BhkOfficeService : IBhkOfficeService
    {
        private readonly AppDB _dbContext;
        
        public BhkOfficeService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetPageLoadData()
        {
            try
            {
                var segmentList = await _dbContext.Tbl_Segment.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive
                }).OrderBy(x => x.Text).ToListAsync();

                BhkOfficeResponseModel response = new()
                {
                    SegmentList = segmentList
                };

                return new ResponseBaseModel(200, response);
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllBhkOffice()
        {
            try
            {
                IQueryable<BhkOfficeModel> query =
                (
                    from BhkOffice in _dbContext.Tbl_BhkOffice
                    
                    join Segment in _dbContext.Tbl_Segment on BhkOffice.SegmentId equals Segment.Id into SegmentTemp
                    from Segment in SegmentTemp.DefaultIfEmpty()
                    
                    orderby BhkOffice.Name ascending

                    select new BhkOfficeModel
                    {
                        Id = BhkOffice.Id,
                        Name = BhkOffice.Name,
                        IsActive = BhkOffice.IsActive,
                        SegmentId = BhkOffice.SegmentId,
                        SegmentName = Segment.Name
                    }
                ).AsQueryable();

                int totalRecords = query.Count();

                List<BhkOfficeModel> result = await query.ToListAsync();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result, totalRecords);
                else
                    return new ResponseBaseModel(404, new List<BhkOfficeModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdateBhkOffice(BhkOfficeModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_BhkOffice> checkValidation = _dbContext.Tbl_BhkOffice.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_BhkOffice> checkValidation = _dbContext.Tbl_BhkOffice.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_BhkOffice record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    SegmentId = request.SegmentId,
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_BhkOffice.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Bhk/office updated successfully.");
                }

                _dbContext.Tbl_BhkOffice.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Bhk/office updated successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
        
        public async Task<ResponseBaseModel> DeleteBhkOffice(int id)
        {
            try
            {
                Tbl_BhkOffice? record = await _dbContext.Tbl_BhkOffice.FirstOrDefaultAsync(x => x.Id == id);
                if (record != null)
                    _dbContext.Tbl_BhkOffice.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Bhk/office deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the Bhk/office. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
