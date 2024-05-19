using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class AreasService : IAreaService
    {
        private readonly AppDB _dbContext;

        public AreasService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> GetAllAreas()
        {
            try
            {
                var result = await _dbContext.Tbl_Area.Select(record => new MasterCommonFieldsModel
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
        
        public async Task<ResponseBaseModel> AddOrUpdateArea(MasterCommonFieldsModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    IQueryable<Tbl_Area> checkValidation = _dbContext.Tbl_Area.Where(x => x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists.");
                }
                else
                {
                    IQueryable<Tbl_Area> checkValidation = _dbContext.Tbl_Area.Where(x => x.Id != request.Id && x.Name == request.Name);

                    if (checkValidation.Any())
                        return new ResponseBaseModel(500, "Name is already exists with another record.");
                }

                Tbl_Area area = new()
                {
                    Name = request.Name,
                    IsActive = request.IsActive
                };

                if (request.Id != null && request.Id != 0)
                {
                    area.Id = Convert.ToInt32(request.Id);
                    _dbContext.Tbl_Area.Update(area);
                    await _dbContext.SaveChangesAsync();
                    
                    return new ResponseBaseModel(200, "Area updated successfully.");
                }

                _dbContext.Tbl_Area.Add(area);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Area added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
        
        public async Task<ResponseBaseModel> DeleteArea(int id)
        {
            try
            {
                Tbl_Area? area = await _dbContext.Tbl_Area.FirstOrDefaultAsync(x => x.Id == id);

                if (area != null)
                    _dbContext.Tbl_Area.Remove(area);
                else
                    return new ResponseBaseModel(404, "Record not found.");

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Area deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the area. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
