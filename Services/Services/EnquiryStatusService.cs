using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class EnquiryStatusService : IEnquiryStatusService
    {
        private readonly AppDB _dbContext;
        
        public EnquiryStatusService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllEnquiryStatus()
        {
            try
            {
                var result = await _dbContext.Tbl_EnquiryStatus.Select(record => new MasterCommonFieldsModel
                {
                    Id = record.Id,
                    IsActive = record.IsActive,
                    Name = record.Name
                }).OrderBy(x => x.Id).ToListAsync();

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

        public async Task<ResponseBaseModel> AddOrUpdateEnquiryStatus(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_EnquiryStatus> checkValidation = _dbContext.Tbl_EnquiryStatus.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    if(request.Id == 1 || request.Id == 2 || request.Id == 3)
                    {
                        return new ResponseBaseModel(500, "Update is not allowed with Open, Close and Hold status.");
                    }

                    IQueryable<Tbl_EnquiryStatus> checkValidation = _dbContext.Tbl_EnquiryStatus.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_EnquiryStatus record = new();

                record.Name = request.Name;
                record.IsActive = request.IsActive;

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_EnquiryStatus.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Enquiry status updated successfully.");
                }

                _dbContext.Tbl_EnquiryStatus.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Enquiry status added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteEnquiryStatus(int id)
        {
            try
            {
                if (id == 1 || id == 2 || id == 3)
                {
                    return new ResponseBaseModel(500, "Delete is not allowed with Open, Close and Hold status.");
                }

                Tbl_EnquiryStatus? record = await _dbContext.Tbl_EnquiryStatus.FirstOrDefaultAsync(x => x.Id == id);
                
                if (record != null)
                    _dbContext.Tbl_EnquiryStatus.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Enquiry status deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the enquiry status. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
