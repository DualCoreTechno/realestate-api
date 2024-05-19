using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Settings;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class EnquiryService : IEnquiryService
    {
        private readonly AppDB _dbContext;
        private readonly IUserChannelService _userChannelService;

        public EnquiryService(AppDB dbContext, IUserChannelService userChannelService)
        {
            _dbContext = dbContext;
            _userChannelService = userChannelService;
        }

        public async Task<ResponseBaseModel> GetPageLoadDataAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    var sourceList = await _dbContext.Tbl_Source.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive,
                    }).OrderBy(x => x.Text).ToListAsync();

                    List<Tbl_BhkOffice> bhkOfficeList = await _dbContext.Tbl_BhkOffice.OrderBy(x => x.Name).Include(x => x.Segment).ThenInclude(x => x.PropertyType).ToListAsync();

                    var budgetList = await _dbContext.Tbl_Budget.Select(record => new BudgetDropdownModel
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive,
                        For = record.For,
                        BhkOfficeId = record.BhkOfficeId
                    }).OrderBy(x => x.Text).ToListAsync();

                    var nonuseList = await _dbContext.Tbl_Nonuse.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive
                    }).OrderBy(x => x.Text).ToListAsync();

                    var areaList = await _dbContext.Tbl_Area.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive
                    }).OrderBy(x => x.Text).ToListAsync();

                    var enquiryStatusList = await _dbContext.Tbl_EnquiryStatus.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive
                    }).OrderBy(x => x.Id).ToListAsync();

                    var activityParentList = await _dbContext.Tbl_ActivityParent.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive
                    }).OrderBy(x => x.Text).ToListAsync();

                    var activityChildList = await _dbContext.Tbl_ActivityChild.Select(record => new ActivityDropDownResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive,
                        ActivityParentId = record.ActivityParentId
                    }).OrderBy(x => x.Text).ToListAsync();

                    List<Tbl_Users> userList;

                    if (userRole == RoleEnum.Admin.ToString())
                    {
                        userList = await _dbContext.Tbl_Users.ToListAsync();
                    }
                    else
                    {
                        int[] userChannel = await _userChannelService.GetUserChannelAsync(userId);
                        userList = await _dbContext.Tbl_Users.Where(x => userChannel.Contains(x.UserId)).OrderBy(x => x.FirstName).ToListAsync();
                    }

                    EnquiryPageLoadModel response = new()
                    {
                        SourceList = sourceList,

                        BhkOfficeList = bhkOfficeList.Select(record => new DropDownCommonResponse
                        {
                            Id = record.Id,
                            Text = $"{record.Name} - {record.Segment.Name} - {record.Segment.PropertyType.Name}",
                            IsActive = record.IsActive,
                        }).ToList(),

                        BudgetList = budgetList,

                        NonuseList = nonuseList,

                        AreaList = areaList,

                        EnquiryStatusList = enquiryStatusList,

                        ActivityParentList = activityParentList,

                        ActivityChildList = activityChildList,

                        UserList = userList.Select(record => new DropDownCommonResponse
                        {
                            Id = record.UserId,
                            Text = $"{record.FirstName} {record.LastName}",
                            IsActive = record.IsActive,
                        }).ToList(),
                    };

                    return new ResponseBaseModel(200, response);
                }
                else
                {
                    return new ResponseBaseModel(404, new EnquiryPageLoadModel(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllEnquiryAsync(EnquiryRequestModel enquiryRequestModel, int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    int[]? userChannel = null;

                    if (userRole != RoleEnum.Admin.ToString())
                        userChannel = await _userChannelService.GetUserChannelAsync(userId);

                    DateTime? fromCreatedOn = null;
                    DateTime? toCreatedOn = null;

                    if (!String.IsNullOrEmpty(enquiryRequestModel.FromNfd))
                        fromCreatedOn = DateConverter.ToDateTime(enquiryRequestModel.FromNfd)?.Date;

                    if (!String.IsNullOrEmpty(enquiryRequestModel.ToNfd))
                        toCreatedOn = DateConverter.ToDateTime(enquiryRequestModel.ToNfd)?.Date.AddDays(1).AddSeconds(-1);

                    var searchKeyword = enquiryRequestModel.Text.ToLower().Trim();

                    IQueryable<EnquiryModel> query =
                    (
                        from Enquiry in _dbContext.Tbl_Enquiry
                        where (userChannel == null || userChannel.Contains(Enquiry.AssignTo)) &&
                            (enquiryRequestModel.EnquiryFor <= 0 || Enquiry.EnquiryFor == enquiryRequestModel.EnquiryFor) &&
                            (enquiryRequestModel.BhkOfficeId <= 0 || Enquiry.BhkOfficeId == enquiryRequestModel.BhkOfficeId) &&
                            (enquiryRequestModel.BudgetId <= 0 || Enquiry.BudgetId == enquiryRequestModel.BudgetId) &&
                            (enquiryRequestModel.SourceId <= 0 || Enquiry.SourceId == enquiryRequestModel.SourceId) &&
                            (enquiryRequestModel.EnquiryStatusId <= 0 || Enquiry.EnquiryStatusId == enquiryRequestModel.EnquiryStatusId) &&
                            (enquiryRequestModel.NonuseId <= 0 || Enquiry.NonuseId == enquiryRequestModel.NonuseId) &&
                            (enquiryRequestModel.EmployeeId <= 0 || Enquiry.AssignTo == enquiryRequestModel.EmployeeId) &&
                            (!fromCreatedOn.HasValue || Enquiry.CreatedOn >= fromCreatedOn) &&
                            (!toCreatedOn.HasValue || Enquiry.CreatedOn <= toCreatedOn) &&
                            (String.IsNullOrEmpty(searchKeyword) ||
                                 ((!String.IsNullOrEmpty(Enquiry.NameOfClient) && Enquiry.NameOfClient.ToLower().Contains(searchKeyword)) ||
                                 (!String.IsNullOrEmpty(Enquiry.MobileNo) && Enquiry.MobileNo.Contains(searchKeyword)) ||
                                 (!String.IsNullOrEmpty(Enquiry.Email) && Enquiry.Email.ToLower().Contains(searchKeyword)) ||
                                 (!String.IsNullOrEmpty(Enquiry.Mobile1) && Enquiry.Mobile1.Contains(searchKeyword)) ||
                                 (!String.IsNullOrEmpty(Enquiry.Mobile2) && Enquiry.Mobile2.Contains(searchKeyword)) ||
                                 (!String.IsNullOrEmpty(Enquiry.Mobile3) && Enquiry.Mobile3.Contains(searchKeyword)) ||
                                 (!String.IsNullOrEmpty(Enquiry.Remark) && Enquiry.Remark.ToLower().Contains(searchKeyword)) ||
                                 (!String.IsNullOrEmpty(Enquiry.LastRemark) && Enquiry.LastRemark.ToLower().Contains(searchKeyword))))

                        join BhkOffice in _dbContext.Tbl_BhkOffice.Include(x => x.Segment) on Enquiry.BhkOfficeId equals BhkOffice.Id into BhkOfficeTemp
                        from BhkOffice in BhkOfficeTemp.DefaultIfEmpty()

                        join User in _dbContext.Tbl_Users on Enquiry.AssignTo equals User.UserId into UserTemp
                        from User in UserTemp.DefaultIfEmpty()

                        join AssignByUser in _dbContext.Tbl_Users on Enquiry.AssignBy equals AssignByUser.UserId into AssignByUserTemp
                        from AssignByUser in AssignByUserTemp.DefaultIfEmpty()

                        join EnquiryStatus in _dbContext.Tbl_EnquiryStatus on Enquiry.EnquiryStatusId equals EnquiryStatus.Id into EnquiryStatusTemp
                        from EnquiryStatus in EnquiryStatusTemp.DefaultIfEmpty()

                        join Source in _dbContext.Tbl_Source on Enquiry.SourceId equals Source.Id into SourceTemp
                        from Source in SourceTemp.DefaultIfEmpty()

                        join Budget in _dbContext.Tbl_Budget on Enquiry.BudgetId equals Budget.Id into BudgetTemp
                        from Budget in BudgetTemp.DefaultIfEmpty()

                        join Nonuse in _dbContext.Tbl_Nonuse on Enquiry.NonuseId equals Nonuse.Id into NonuseTemp
                        from Nonuse in NonuseTemp.DefaultIfEmpty()

                        orderby Enquiry.CreatedOn descending

                        select new EnquiryModel
                        {
                            Id = Enquiry.Id,
                            NameOfClient = Enquiry.NameOfClient,
                            MobileNo = Enquiry.MobileNo,
                            Email = Enquiry.Email,
                            EnquiryFor = Enquiry.EnquiryFor,
                            SourceId = Enquiry.SourceId,
                            SourceName = Source.Name,
                            BhkOfficeId = Enquiry.BhkOfficeId,
                            BhkOfficeName = BhkOffice.Name,
                            SegmentName = BhkOffice.Segment == null ? "" : BhkOffice.Segment.Name,
                            PropertyTypeName = BhkOffice.Segment == null ? "" : BhkOffice.Segment.PropertyType == null ? "" : BhkOffice.Segment.PropertyType.Name,
                            EnquiryStatusId = Enquiry.EnquiryStatusId,
                            EnquiryStatusName = EnquiryStatus.Name,
                            BudgetId = Enquiry.BudgetId,
                            BudgetName = Budget.Name,
                            NonuseId = Enquiry.NonuseId,
                            NonuseName = Nonuse.Name,
                            AreaId = Enquiry.AreaId,
                            Remark = Enquiry.Remark,
                            LastRemark = String.IsNullOrEmpty(Enquiry.LastRemark) ? Enquiry.Remark : Enquiry.LastRemark,
                            AssignTo = Enquiry.AssignTo,
                            AssignToName = $"{User.FirstName} {User.LastName}",
                            AssignBy = Enquiry.AssignBy,
                            AssignByName = $"{AssignByUser.FirstName} {AssignByUser.LastName}",
                            Nfd = DateConverter.ToStringDateTime(Enquiry.Nfd),
                            //IsClosed = Enquiry.IsClosed,
                            Mobile1 = Enquiry.Mobile1,
                            Mobile2 = Enquiry.Mobile2,
                            Mobile3 = Enquiry.Mobile3,
                            CreatedOn = DateConverter.ToStringDateTime(Enquiry.CreatedOn)
                        }
                    ).AsQueryable();

                    int totalRecords = query.Count();

                    List<EnquiryModel> result = new();

                    if (enquiryRequestModel.RowPerPage > 0)
                        result = await query.Skip(enquiryRequestModel.Offset).Take(enquiryRequestModel.RowPerPage).ToListAsync();

                    if (result.Count > 0)
                        return new ResponseBaseModel(200, result, totalRecords);
                    else
                        return new ResponseBaseModel(404, new List<EnquiryModel>(), "No record(s) found.");
                }
                else
                {
                    return new ResponseBaseModel(404, new List<EnquiryModel>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetEnquiryForDashboardAsync(DashboardRequestModel dashboardRequestModel, int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    int[]? userChannel = null;

                    if (userRole != RoleEnum.Admin.ToString())
                        userChannel = await _userChannelService.GetUserChannelAsync(userId);

                    DateTime? todayFromDate = DateConverter.GetSystemDateTime().Date;
                    DateTime? todayToDate = DateConverter.GetSystemDateTime().Date.AddDays(1).AddSeconds(-1);

                    DateTime? tomorrowFromDate = DateConverter.GetSystemDateTime().Date.AddDays(1);
                    DateTime? tomorrowToDate = DateConverter.GetSystemDateTime().Date.AddDays(2).AddSeconds(-1);

                    var searchKeyword = dashboardRequestModel.Text.ToLower().Trim();

                    IQueryable<EnquiryModel> query =
                    (
                        from Enquiry in _dbContext.Tbl_Enquiry
                        where (userChannel == null || userChannel.Contains(Enquiry.AssignTo)) &&
                        (Enquiry.EnquiryStatusId != 2) &&
                        (
                            (dashboardRequestModel.NfdType == DashboardFilterEnum.Today && (Enquiry.Nfd >= todayFromDate && Enquiry.Nfd <= todayToDate)) ||
                            (dashboardRequestModel.NfdType == DashboardFilterEnum.Tomorrow && (Enquiry.Nfd >= tomorrowFromDate && Enquiry.Nfd <= tomorrowToDate)) ||
                            (dashboardRequestModel.NfdType == DashboardFilterEnum.Pending && Enquiry.Nfd < todayFromDate)
                        ) &&
                        (String.IsNullOrEmpty(searchKeyword) ||
                                ((!String.IsNullOrEmpty(Enquiry.NameOfClient) && Enquiry.NameOfClient.ToLower().Contains(searchKeyword)) ||
                                (!String.IsNullOrEmpty(Enquiry.MobileNo) && Enquiry.MobileNo.Contains(searchKeyword)) ||
                                (!String.IsNullOrEmpty(Enquiry.Email) && Enquiry.Email.ToLower().Contains(searchKeyword)) ||
                                (!String.IsNullOrEmpty(Enquiry.Mobile1) && Enquiry.Mobile1.Contains(searchKeyword)) ||
                                (!String.IsNullOrEmpty(Enquiry.Mobile2) && Enquiry.Mobile2.Contains(searchKeyword)) ||
                                (!String.IsNullOrEmpty(Enquiry.Mobile3) && Enquiry.Mobile3.Contains(searchKeyword)) ||
                                (!String.IsNullOrEmpty(Enquiry.Remark) && Enquiry.Remark.ToLower().Contains(searchKeyword)) ||
                                (!String.IsNullOrEmpty(Enquiry.LastRemark) && Enquiry.LastRemark.ToLower().Contains(searchKeyword))))

                        join BhkOffice in _dbContext.Tbl_BhkOffice.Include(x => x.Segment) on Enquiry.BhkOfficeId equals BhkOffice.Id into BhkOfficeTemp
                        from BhkOffice in BhkOfficeTemp.DefaultIfEmpty()

                        join User in _dbContext.Tbl_Users on Enquiry.AssignTo equals User.UserId into UserTemp
                        from User in UserTemp.DefaultIfEmpty()

                        join AssignByUser in _dbContext.Tbl_Users on Enquiry.AssignBy equals AssignByUser.UserId into AssignByUserTemp
                        from AssignByUser in AssignByUserTemp.DefaultIfEmpty()

                        join EnquiryStatus in _dbContext.Tbl_EnquiryStatus on Enquiry.EnquiryStatusId equals EnquiryStatus.Id into EnquiryStatusTemp
                        from EnquiryStatus in EnquiryStatusTemp.DefaultIfEmpty()

                        join Source in _dbContext.Tbl_Source on Enquiry.SourceId equals Source.Id into SourceTemp
                        from Source in SourceTemp.DefaultIfEmpty()

                        join Budget in _dbContext.Tbl_Budget on Enquiry.BudgetId equals Budget.Id into BudgetTemp
                        from Budget in BudgetTemp.DefaultIfEmpty()

                        join Nonuse in _dbContext.Tbl_Nonuse on Enquiry.NonuseId equals Nonuse.Id into NonuseTemp
                        from Nonuse in NonuseTemp.DefaultIfEmpty()

                        orderby Enquiry.CreatedOn descending

                        select new EnquiryModel
                        {
                            Id = Enquiry.Id,
                            NameOfClient = Enquiry.NameOfClient,
                            MobileNo = Enquiry.MobileNo,
                            Email = Enquiry.Email,
                            EnquiryFor = Enquiry.EnquiryFor,
                            SourceId = Enquiry.SourceId,
                            SourceName = Source.Name,
                            BhkOfficeId = Enquiry.BhkOfficeId,
                            BhkOfficeName = BhkOffice.Name,
                            SegmentName = BhkOffice.Segment == null ? "" : BhkOffice.Segment.Name,
                            PropertyTypeName = BhkOffice.Segment == null ? "" : BhkOffice.Segment.PropertyType == null ? "" : BhkOffice.Segment.PropertyType.Name,
                            EnquiryStatusId = Enquiry.EnquiryStatusId,
                            EnquiryStatusName = EnquiryStatus.Name,
                            BudgetId = Enquiry.BudgetId,
                            BudgetName = Budget.Name,
                            NonuseId = Enquiry.NonuseId,
                            NonuseName = Nonuse.Name,
                            AreaId = Enquiry.AreaId,
                            //AreaName = Area.Name,
                            Remark = Enquiry.Remark,
                            LastRemark = String.IsNullOrEmpty(Enquiry.LastRemark) ? Enquiry.Remark : Enquiry.LastRemark,
                            AssignTo = Enquiry.AssignTo,
                            AssignToName = $"{User.FirstName} {User.LastName}",
                            AssignBy = Enquiry.AssignBy,
                            AssignByName = $"{AssignByUser.FirstName} {AssignByUser.LastName}",
                            Nfd = DateConverter.ToStringDateTime(Enquiry.Nfd),
                            //IsClosed = Enquiry.IsClosed,
                            Mobile1 = Enquiry.Mobile1,
                            Mobile2 = Enquiry.Mobile2,
                            Mobile3 = Enquiry.Mobile3,
                            CreatedOn = DateConverter.ToStringDateTime(Enquiry.CreatedOn)
                        }
                    ).AsQueryable();

                    IQueryable<EnquiryModel> todaysQuery =
                    (
                        from Enquiry in _dbContext.Tbl_Enquiry
                        where (userChannel == null || userChannel.Contains(Enquiry.AssignTo)) &&
                            Enquiry.EnquiryStatusId != 2 && Enquiry.Nfd >= todayFromDate && Enquiry.Nfd <= todayToDate

                        select new EnquiryModel
                        {
                            Id = Enquiry.Id
                        }
                    ).AsQueryable();

                    IQueryable<EnquiryModel> tomorrowsQuery =
                    (
                        from Enquiry in _dbContext.Tbl_Enquiry
                        where (userChannel == null || userChannel.Contains(Enquiry.AssignTo)) &&
                            Enquiry.EnquiryStatusId != 2 && Enquiry.Nfd >= tomorrowFromDate && Enquiry.Nfd <= tomorrowToDate

                        select new EnquiryModel
                        {
                            Id = Enquiry.Id
                        }
                    ).AsQueryable();

                    IQueryable<EnquiryModel> pendingQuery =
                    (
                        from Enquiry in _dbContext.Tbl_Enquiry
                        where (userChannel == null || userChannel.Contains(Enquiry.AssignTo)) && Enquiry.EnquiryStatusId != 2 && Enquiry.Nfd < todayFromDate

                        select new EnquiryModel
                        {
                            Id = Enquiry.Id
                        }
                    ).AsQueryable();

                    int totalRecords = query.Count();

                    List<EnquiryModel> result = new();

                    if (dashboardRequestModel.RowPerPage > 0)
                        result = await query.Skip(dashboardRequestModel.Offset).Take(dashboardRequestModel.RowPerPage).ToListAsync();

                    var finalResponse = new EnquiryResponseModel
                    {
                        TodaysCount = todaysQuery.Count(),
                        TomorrowsCount = tomorrowsQuery.Count(),
                        PendingCount = pendingQuery.Count(),
                        EnquiryList = result
                    };

                    return new ResponseBaseModel(200, finalResponse, totalRecords);
                }
                else
                {
                    return new ResponseBaseModel(404, new List<EnquiryModel>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> CheckMobileExistsAsync(EnquiryCheckMobileModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    var isMobileNumbeAlreadyExist = await _dbContext.Tbl_Enquiry.AnyAsync(x => x.MobileNo == request.MobileNo ||
                                    x.Mobile1 == request.MobileNo || x.Mobile2 == request.MobileNo || x.Mobile3 == request.MobileNo);

                    if (isMobileNumbeAlreadyExist)
                    {
                        return new ResponseBaseModel(409, "Mobile number is already exists with other enquiry.");
                    }
                }
                else
                {
                    var isMobileNumbeAlreadyExist = await _dbContext.Tbl_Enquiry.AnyAsync(x => x.Id != request.Id && x.MobileNo == request.MobileNo ||
                            x.Mobile1 == request.MobileNo || x.Mobile2 == request.MobileNo || x.Mobile3 == request.MobileNo);

                    if (isMobileNumbeAlreadyExist)
                    {
                        return new ResponseBaseModel(409, "Mobile number is already exists with other enquiry.");
                    }
                }

                return new ResponseBaseModel(200, "Mobile number is available.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong during mobile number checking.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdateEnquiryAsync(EnquiryModel request)
        {
            try
            {
                if (request.Id == 0)
                {
                    var isMobileNumbeAlreadyExist = await _dbContext.Tbl_Enquiry.AnyAsync(x =>
                        ((!string.IsNullOrEmpty(x.MobileNo) &&
                            (x.MobileNo == request.MobileNo || x.MobileNo == request.Mobile1 || x.MobileNo == request.Mobile2 || x.MobileNo == request.Mobile3)) ||
                         (!string.IsNullOrEmpty(x.Mobile1) &&
                            (x.Mobile1 == request.MobileNo || x.Mobile1 == request.Mobile1 || x.Mobile1 == request.Mobile2 || x.Mobile1 == request.Mobile3)) ||
                         (!string.IsNullOrEmpty(x.Mobile2) &&
                            (x.Mobile2 == request.MobileNo || x.Mobile2 == request.Mobile1 || x.Mobile2 == request.Mobile2 || x.Mobile2 == request.Mobile3)) ||
                         (!string.IsNullOrEmpty(x.Mobile3) &&
                            (x.Mobile3 == request.MobileNo || x.Mobile3 == request.Mobile1 || x.Mobile3 == request.Mobile2 || x.Mobile3 == request.Mobile3))));

                    if (isMobileNumbeAlreadyExist)
                    {
                        return new ResponseBaseModel(500, "Mobile number is already exists with other enquiry.");
                    }
                }
                else
                {
                    var isMobileNumbeAlreadyExist = await _dbContext.Tbl_Enquiry.AnyAsync(x => x.Id != request.Id &&
                       ((!string.IsNullOrEmpty(x.MobileNo) &&
                           (x.MobileNo == request.MobileNo || x.MobileNo == request.Mobile1 || x.MobileNo == request.Mobile2 || x.MobileNo == request.Mobile3)) ||
                        (!string.IsNullOrEmpty(x.Mobile1) &&
                           (x.Mobile1 == request.MobileNo || x.Mobile1 == request.Mobile1 || x.Mobile1 == request.Mobile2 || x.Mobile1 == request.Mobile3)) ||
                        (!string.IsNullOrEmpty(x.Mobile2) &&
                           (x.Mobile2 == request.MobileNo || x.Mobile2 == request.Mobile1 || x.Mobile2 == request.Mobile2 || x.Mobile2 == request.Mobile3)) ||
                        (!string.IsNullOrEmpty(x.Mobile3) &&
                           (x.Mobile3 == request.MobileNo || x.Mobile3 == request.Mobile1 || x.Mobile3 == request.Mobile2 || x.Mobile3 == request.Mobile3))));

                    if (isMobileNumbeAlreadyExist)
                    {
                        return new ResponseBaseModel(500, "Mobile number is already exists with other enquiry.");
                    }
                }

                Tbl_Enquiry record = new()
                {
                    Id = request.Id,
                    NameOfClient = request.NameOfClient,
                    MobileNo = request.MobileNo,
                    Email = request.Email,
                    EnquiryFor = request.EnquiryFor,
                    SourceId = request.SourceId,
                    BhkOfficeId = request.BhkOfficeId,
                    EnquiryStatusId = request.EnquiryStatusId,
                    BudgetId = request.BudgetId,
                    NonuseId = request.NonuseId,
                    AreaId = request.AreaId,
                    Remark = request.Remark,
                    AssignTo = request.AssignTo,
                    AssignBy = request.AssignBy,
                    Nfd = DateConverter.ToDateTime(request.Nfd),
                    //IsClosed = request.IsClosed,
                    Mobile1 = request.Mobile1,
                    Mobile2 = request.Mobile2,
                    Mobile3 = request.Mobile3,
                };

                if (request.Id != 0)
                {
                    _dbContext.Tbl_Enquiry.Update(record);
                    _dbContext.Entry(record).Property(x => x.LastRemark).IsModified = false;
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Enquiry updated successfully.");
                }
                else
                {
                    await _dbContext.Tbl_Enquiry.AddAsync(record);
                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Enquiry added successfully.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddEnquiryRemarksAsync(EnquiryRemarksModel request)
        {
            try
            {
                if (request.EnquiryId <= 0)
                {
                    return new ResponseBaseModel(500, "Enquiry is required");
                }
                else if (string.IsNullOrEmpty(request.Nfd))
                {
                    return new ResponseBaseModel(500, "Nfd is required");
                }
                else if (request.EnquiryStatusId <= 0)
                {
                    return new ResponseBaseModel(500, "Enquiry status is required");
                }

                var enquiryInfo = await _dbContext.Tbl_Enquiry.FirstOrDefaultAsync(x => x.Id == request.EnquiryId);

                if (enquiryInfo != null)
                {
                    Tbl_EnquiryRemarks record = new()
                    {
                        EnquiryId = request.EnquiryId,
                        ActivityChildId = request.ActivityChildId,
                        Nfd = DateConverter.ToDateTime(request.Nfd),
                        EnquiryStatusId = request.EnquiryStatusId,
                        Remark = request.Remark,
                        CreatedBy = request.CreatedBy,
                    };

                    await _dbContext.Tbl_EnquiryRemarks.AddAsync(record);

                    enquiryInfo.Nfd = DateConverter.ToDateTime(request.Nfd);
                    enquiryInfo.EnquiryStatusId = request.EnquiryStatusId;
                    enquiryInfo.LastRemark = request.Remark;
                    //enquiryInfo.IsClosed = request.IsClosed;

                    _dbContext.Tbl_Enquiry.Update(enquiryInfo);

                    await _dbContext.SaveChangesAsync();

                    return new ResponseBaseModel(200, "Enquiry remark added successfully.");
                }

                return new ResponseBaseModel(404, "Enquiry not found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetEnquiryRemarksAsync(int enquiryId)
        {
            try
            {
                IQueryable<EnquiryRemarksModel> query =
                (
                    from EnquiryRemarks in _dbContext.Tbl_EnquiryRemarks
                    where EnquiryRemarks.EnquiryId == enquiryId
                    join ActivityChild in _dbContext.Tbl_ActivityChild on EnquiryRemarks.ActivityChildId equals ActivityChild.Id
                    join EnquiryStatus in _dbContext.Tbl_EnquiryStatus on EnquiryRemarks.EnquiryStatusId equals EnquiryStatus.Id
                    join CreateByUser in _dbContext.Tbl_Users on EnquiryRemarks.CreatedBy equals CreateByUser.UserId

                    orderby EnquiryRemarks.CreatedOn descending

                    select new EnquiryRemarksModel
                    {
                        EnquiryId = EnquiryRemarks.EnquiryId,
                        ActivityChildId = EnquiryRemarks.ActivityChildId,
                        ActivityChildName = ActivityChild.ActivityParent == null ? $"{ActivityChild.Name}" : $"{ActivityChild.ActivityParent.Name} - {ActivityChild.Name}",
                        Nfd = DateConverter.ToStringDateTime(EnquiryRemarks.Nfd),
                        EnquiryStatusId = EnquiryRemarks.EnquiryStatusId,
                        EnquiryStatusName = EnquiryStatus.Name,
                        Remark = EnquiryRemarks.Remark,
                        CreatedBy = EnquiryRemarks.CreatedBy,
                        CreatedByName = $"{CreateByUser.FirstName} {CreateByUser.LastName}",
                        CreatedOn = DateConverter.ToStringDateTime(EnquiryRemarks.CreatedOn)
                    }
                ).AsQueryable();

                int totalRecords = query.Count();

                List<EnquiryRemarksModel> result = await query.ToListAsync();

                if (result.Count > 0)
                    return new ResponseBaseModel(200, result, totalRecords);
                else
                    return new ResponseBaseModel(404, new List<EnquiryRemarksModel>(), "No record(s) found.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeleteEnquiryAsync(int id)
        {
            try
            {
                Tbl_Enquiry? record = await _dbContext.Tbl_Enquiry.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                {
                    List<Tbl_EnquiryRemarks>? remarks = await _dbContext.Tbl_EnquiryRemarks.Where(x => x.EnquiryId == record.Id).ToListAsync();

                    if (remarks != null && remarks.Count > 0)
                        _dbContext.Tbl_EnquiryRemarks.RemoveRange(remarks);

                    _dbContext.Tbl_Enquiry.Remove(record);
                }
                else
                {
                    return new ResponseBaseModel(404, "Record not found.");
                }

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Enquiry deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the Enquiry. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public ResponseBaseModel SendMail(MailModel mailModel)
        {
            try
            {
                if (string.IsNullOrEmpty(mailModel.RecipientEmail))
                {
                    return new ResponseBaseModel(500, "Mail id is required");
                }

                bool result = MailSender.SendEmail(mailModel.RecipientEmail, mailModel.MailSubject, mailModel.MailBody, false, null, null, null, null, null);

                if (result)
                {
                    return new ResponseBaseModel(200, "Mail send successfully.");
                }
                else
                {
                    return new ResponseBaseModel(500, "Something went wrong, please try again.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
