using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Settings;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDB _dbContext;
        private readonly IUserChannelService _userChannelService;

        public PropertyService(AppDB dbContext, IUserChannelService userChannelService)
        {
            _dbContext = dbContext;
            _userChannelService = userChannelService;
        }

        public async Task<ResponseBaseModel> GetPageLoadDataAsync()
        {
            try
            {
                var propertyTypeList = await _dbContext.Tbl_PropertyType.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                var segmentList = await _dbContext.Tbl_Segment.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                List<Tbl_BhkOffice> bhkOfficeList = await _dbContext.Tbl_BhkOffice.OrderBy(x => x.Name).Include(x => x.Segment).ThenInclude(x => x.PropertyType).ToListAsync();

                var buildingsList = await _dbContext.Tbl_Buildings.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                var areaList = await _dbContext.Tbl_Area.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                var measurementList = await _dbContext.Tbl_Measurement.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                var furnitureStatusList = await _dbContext.Tbl_FurnitureStatus.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                var propertyStatusList = await _dbContext.Tbl_PropertyStatus.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                var sourceList = await _dbContext.Tbl_Source.Select(record => new DropDownCommonResponse
                {
                    Id = record.Id,
                    Text = record.Name,
                    IsActive = record.IsActive,
                }).OrderBy(x => x.Text).ToListAsync();

                //List<Tbl_PropertyType> propertyTypeList = await _dbContext.Tbl_PropertyType.OrderBy(x => x.Name).ToListAsync();
                //List<Tbl_Segment> segmentList = await _dbContext.Tbl_Segment.OrderBy(x => x.Name).ToListAsync();
                //List<Tbl_BhkOffice> bhkOfficeList = await _dbContext.Tbl_BhkOffice.OrderBy(x => x.Name).Include(x => x.Segment).ToListAsync();
                //List<Tbl_Buildings> buildingsList = await _dbContext.Tbl_Buildings.OrderBy(x => x.Name).ToListAsync();
                //List<Tbl_Area> areaList = await _dbContext.Tbl_Area.OrderBy(x => x.Name).ToListAsync();
                //List<Tbl_Measurement> measurementList = await _dbContext.Tbl_Measurement.OrderBy(x => x.Name).ToListAsync();
                //List<Tbl_FurnitureStatus> furnitureStatusList = await _dbContext.Tbl_FurnitureStatus.OrderBy(x => x.Name).ToListAsync();
                //List<Tbl_PropertyStatus> propertyStatusList = await _dbContext.Tbl_PropertyStatus.OrderBy(x => x.Name).ToListAsync();
                //List<Tbl_Source> sourceList = await _dbContext.Tbl_Source.OrderBy(x => x.Name).ToListAsync();

                PropertyPageLoadModel response = new()
                {
                    PropertyTypeList = propertyTypeList,

                    SegmentList = segmentList,

                    BhkOfficeList = bhkOfficeList.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = $"{record.Name} - {record.Segment.Name} - {record.Segment.PropertyType.Name}",
                        IsActive = record.IsActive,
                    }).ToList(),

                    BuildingList = buildingsList,

                    AreaList = areaList,

                    MeasurementList = measurementList,

                    FurnitureStatusList = furnitureStatusList,

                    PropertyStatusList = propertyStatusList,

                    SourceList = sourceList,
                };

                return new ResponseBaseModel(200, response);
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllPropertyAsync(PropertyRequestModel propertyRequestModel, int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    int[]? userChannel = null;

                    if (userRole != RoleEnum.Admin.ToString())
                        userChannel = await _userChannelService.GetUserChannelAsync(userId);

                    DateTime? availableFromDate = null;
                    DateTime? availableToDate = null;

                    if (!String.IsNullOrEmpty(propertyRequestModel.AvailableFromDate))
                        availableFromDate = DateConverter.ToDateTime(propertyRequestModel.AvailableFromDate)?.Date;

                    if (!String.IsNullOrEmpty(propertyRequestModel.AvailableToDate))
                        availableToDate = DateConverter.ToDateTime(propertyRequestModel.AvailableToDate)?.Date.AddDays(1).AddSeconds(-1);

                    IQueryable<PropertyModel> query =
                    (
                        from Property in _dbContext.Tbl_Property
                        where (userChannel == null || userChannel.Contains(Property.UserId)) &&
                            (propertyRequestModel.PropertyFor <= 0 || Property.PropertyFor == propertyRequestModel.PropertyFor) &&
                            (propertyRequestModel.BhkOfficeId <= 0 || Property.BhkOfficeId == propertyRequestModel.BhkOfficeId) &&
                            (propertyRequestModel.BuildingId <= 0 || Property.BuildingId == propertyRequestModel.BuildingId) &&
                            (propertyRequestModel.PropertyStatusId <= 0 || Property.PropertyStatusId == propertyRequestModel.PropertyStatusId) &&
                            (propertyRequestModel.FurnitureStatusId <= 0 || Property.FurnitureStatusId == propertyRequestModel.FurnitureStatusId) &&
                            (propertyRequestModel.AreaId <= 0 || Property.AreaId == propertyRequestModel.AreaId) &&
                            (propertyRequestModel.PropertyMinPrice <= 0 || Property.PropertyPrice >= propertyRequestModel.PropertyMinPrice) &&
                            (propertyRequestModel.PropertyMaxPrice <= 0 || Property.PropertyPrice <= propertyRequestModel.PropertyMaxPrice) &&
                            (!availableFromDate.HasValue || Property.AvailableFrom >= availableFromDate) &&
                            (!availableToDate.HasValue || Property.AvailableFrom <= availableToDate)

                        join BhkOffice in _dbContext.Tbl_BhkOffice.Include(x => x.Segment) on Property.BhkOfficeId equals BhkOffice.Id into BhkOfficeTemp
                        from BhkOffice in BhkOfficeTemp.DefaultIfEmpty()
                        
                        join Buildings in _dbContext.Tbl_Buildings on Property.BuildingId equals Buildings.Id into BuildingsTemp
                        from Buildings in BuildingsTemp.DefaultIfEmpty()
                        
                        join Measurement in _dbContext.Tbl_Measurement on Property.MeasurementId equals Measurement.Id into MeasurementTemp
                        from Measurement in MeasurementTemp.DefaultIfEmpty()
                        
                        join PropertyStatus in _dbContext.Tbl_PropertyStatus on Property.PropertyStatusId equals PropertyStatus.Id into PropertyStatusTemp
                        from PropertyStatus in PropertyStatusTemp.DefaultIfEmpty()

                        join Area in _dbContext.Tbl_Area on Property.AreaId equals Area.Id into AreaTemp
                        from Area in AreaTemp.DefaultIfEmpty()

                        join FurnitureStatus in _dbContext.Tbl_FurnitureStatus on Property.FurnitureStatusId equals FurnitureStatus.Id into FurnitureStatusTemp
                        from FurnitureStatus in FurnitureStatusTemp.DefaultIfEmpty()

                        join Source in _dbContext.Tbl_Source on Property.SourceId equals Source.Id into SourceTemp
                        from Source in SourceTemp.DefaultIfEmpty()

                        orderby Property.CreatedOn descending

                        select new PropertyModel
                        {
                            Id = Property.Id,
                            UserId = Property.UserId,
                            PropertyFor = Property.PropertyFor,
                            BhkOfficeId = Property.BhkOfficeId,
                            BhkOfficeName = BhkOffice.Name,
                            SegmentName = BhkOffice.Segment == null ? "" : BhkOffice.Segment.Name,
                            PropertyTypeName = BhkOffice.Segment == null ? "" : BhkOffice.Segment.PropertyType == null ? "" : BhkOffice.Segment.PropertyType.Name,
                            BuildingId = Property.BuildingId,
                            BuildingName = Buildings.Name,
                            AreaId = Property.AreaId,
                            AreaName = Area.Name,
                            Address = Property.Address,
                            Block = Property.Block,
                            FlatNumber = Property.FlatNumber,
                            MeasurementId = Property.MeasurementId,
                            MeasurementName = Measurement.Name,
                            SuperBuiltupArea = Property.SuperBuiltupArea,
                            CarpetArea = Property.CarpetArea,
                            BuiltupArea = Property.BuiltupArea,
                            FurnitureStatusId = Property.FurnitureStatusId,
                            FurnitureStatusName = FurnitureStatus.Name,
                            Parking = Property.Parking,
                            KeyStatus = Property.KeyStatus,
                            PropertyPrice = Property.PropertyPrice,
                            OwnerName = Property.OwnerName,
                            Mobile = Property.Mobile,
                            Mobile1 = Property.Mobile1,
                            Mobile2 = Property.Mobile2,
                            SourceId = Property.SourceId,
                            SourceName = Source.Name,
                            PropertyStatusId = Property.PropertyStatusId,
                            PropertyStatusName = PropertyStatus.Name,
                            Comission = Property.Comission,
                            Remark = Property.Remark,
                            AvailableFrom = DateConverter.ToStringDateTime(Property.AvailableFrom),
                        }
                    ).AsQueryable();

                    int totalRecords = query.Count();

                    List<PropertyModel> result = new();

                    if (propertyRequestModel.RowPerPage > 0)
                        result = await query.Skip(propertyRequestModel.Offset).Take(propertyRequestModel.RowPerPage).ToListAsync();

                    if (result.Count > 0)
                        return new ResponseBaseModel(200, result, totalRecords);
                    else
                        return new ResponseBaseModel(404, new List<PropertyModel>(), "No record(s) found.");
                }
                else
                {
                    return new ResponseBaseModel(404, new List<PropertyModel>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdatePropertyAsync(PropertyModel request)
        {
            try
            {
                Tbl_Property record = new()
                {
                    Id = request.Id,
                    PropertyFor = request.PropertyFor,
                    BhkOfficeId = request.BhkOfficeId,
                    BuildingId = request.BuildingId,
                    AreaId = request.AreaId,
                    Address = request.Address,
                    Block = request.Block,
                    FlatNumber = request.FlatNumber,
                    MeasurementId = request.MeasurementId,
                    SuperBuiltupArea = request.SuperBuiltupArea,
                    CarpetArea = request.CarpetArea,
                    BuiltupArea = request.BuiltupArea,
                    FurnitureStatusId = request.FurnitureStatusId,
                    Parking = request.Parking,
                    KeyStatus = request.KeyStatus,
                    PropertyPrice = request.PropertyPrice,
                    OwnerName = request.OwnerName,
                    Mobile = request.Mobile,
                    Mobile1 = request.Mobile1,
                    Mobile2 = request.Mobile2,
                    SourceId = request.SourceId,
                    PropertyStatusId = request.PropertyStatusId,
                    Comission = request.Comission,
                    Remark = request.Remark,
                    AvailableFrom = DateConverter.ToDateTime(request.AvailableFrom),
                    UserId = request.UserId
                };

                if (request.Id != 0)
                {
                    _dbContext.Tbl_Property.Update(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Property updated successfully.");
                }
                else
                {
                    await _dbContext.Tbl_Property.AddAsync(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Property added successfully.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeletePropertyAsync(int id)
        {
            try
            {
                Tbl_Property? record = await _dbContext.Tbl_Property.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                {
                    _dbContext.Tbl_Property.Remove(record);
                }
                else
                {
                    return new ResponseBaseModel(404, "Record not found.");
                }

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Property deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the Property. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
