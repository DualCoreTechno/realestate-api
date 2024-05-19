using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class SegmentService : ISegmentService
    {
        private readonly AppDB _dbContext;

        public SegmentService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetPageLoadData()
        {
            try
            {
                var propertyTypeList = await _dbContext.Tbl_PropertyType.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive
                }).OrderBy(x => x.Text).ToListAsync();

                SegmentResponseModel response = new()
                {
                    PropertyTypeList = propertyTypeList
                };

                return new ResponseBaseModel(200, response);
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllSegment()
        {
            try
            {
                IQueryable<SegmentModel> query =
                (
                    from Segment in _dbContext.Tbl_Segment
                    
                    join PropertyType in _dbContext.Tbl_PropertyType on Segment.PropertyTypeId equals PropertyType.Id into PropertyTypeTemp
                    from PropertyType in PropertyTypeTemp.DefaultIfEmpty()
                    
                    orderby Segment.Name ascending

                    select new SegmentModel
                    {
                        Id = Segment.Id,
                        Name = Segment.Name,
                        IsActive = Segment.IsActive,
                        PropertyTypeId = Segment.PropertyTypeId,
                        PropertyTypeName = PropertyType.Name
                    }
                ).AsQueryable();

                int totalRecords = query.Count();

                List<SegmentModel> result = await query.ToListAsync();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result, totalRecords);
                else
                    return new ResponseBaseModel(404, new List<SegmentModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdateSegment(SegmentModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_Segment> checkValidation = _dbContext.Tbl_Segment.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_Segment> checkValidation = _dbContext.Tbl_Segment.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_Segment record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    PropertyTypeId = request.PropertyTypeId
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_Segment.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Segment updated successfully.");
                }

                _dbContext.Tbl_Segment.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Segment added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteSegment(int id)
        {
            try
            {
                Tbl_Segment? record = await _dbContext.Tbl_Segment.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                    _dbContext.Tbl_Segment.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Segment deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the segment. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
