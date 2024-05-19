using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly AppDB _dbContext;

        public PropertyTypeService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllPropertyType()
        {
            try
            {
                var result = await _dbContext.Tbl_PropertyType.Select(record => new MasterCommonFieldsModel
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

        public async Task<ResponseBaseModel> AddOrUpdatePropertyType(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_PropertyType> checkValidation = _dbContext.Tbl_PropertyType.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_PropertyType> checkValidation = _dbContext.Tbl_PropertyType.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_PropertyType record = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    record.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_PropertyType.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Property type updated successfully.");
                }

                _dbContext.Tbl_PropertyType.Add(record);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Property type added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeletePropertyType(int id)
        {
            try
            {
                Tbl_PropertyType? record = await _dbContext.Tbl_PropertyType.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                    _dbContext.Tbl_PropertyType.Remove(record);
                else
                    return new ResponseBaseModel(404, "Record not found");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Property type deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the property type. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
