using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Settings;
using ViewModels.Common;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDB _dbContext;
        private readonly IUserChannelService _userChannelService;

        public ReportService(AppDB dbContext, IUserChannelService userChannelService)
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
                    List<Tbl_Users> userList;

                    if (userRole == RoleEnum.Admin.ToString())
                    {
                        userList = await _dbContext.Tbl_Users.OrderBy(x => x.FirstName).ToListAsync();
                    }
                    else
                    {
                        int[] userChannel = await _userChannelService.GetUserChannelAsync(userId);
                        userList = await _dbContext.Tbl_Users.Where(x => userChannel.Contains(x.UserId)).OrderBy(x => x.FirstName).ToListAsync();
                    }

                    ReportPageLoadModel response = new()
                    {
                        PageList = TableNameArray.TableAliasDictionary.Values.ToList(),

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
                    return new ResponseBaseModel(404, new ReportPageLoadModel(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetLogReportAsync(LogReportRequestModel logReportRequestModel, int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    int[]? userChannel = null;

                    if (userRole != RoleEnum.Admin.ToString())
                        userChannel = await _userChannelService.GetUserChannelAsync(userId);

                    DateTime? fromDate = null;
                    DateTime? toDate = null;

                    if (!String.IsNullOrEmpty(logReportRequestModel.FromDate))
                        fromDate = Convert.ToDateTime(DateConverter.ToDateTime(logReportRequestModel.FromDate)?.Date.ToString());

                    if (!String.IsNullOrEmpty(logReportRequestModel.ToDate))
                        toDate = Convert.ToDateTime(DateConverter.ToDateTime(logReportRequestModel.ToDate)?.Date.AddDays(1).AddSeconds(-1).ToString());

                    IQueryable<LogReportModel> query =
                     (
                         from LogData in _dbContext.Tbl_LogData
                         where (userChannel == null || userChannel.Contains(LogData.CreatedBy)) &&
                             (logReportRequestModel.UserId <= 0 || LogData.CreatedBy == logReportRequestModel.UserId) &&
                             (logReportRequestModel.PageName == "All" || LogData.PageName == logReportRequestModel.PageName) &&
                             (string.IsNullOrEmpty(logReportRequestModel.IpAddress) || LogData.IpAddress == logReportRequestModel.IpAddress) &&
                             (!fromDate.HasValue || LogData.CreatedOn >= fromDate) &&
                             (!toDate.HasValue || LogData.CreatedOn <= toDate)

                         join Users in _dbContext.Tbl_Users on LogData.CreatedBy equals Users.UserId into UsersTemp
                         from Users in UsersTemp.DefaultIfEmpty()
                         
                         orderby LogData.CreatedOn descending

                         select new LogReportModel
                         {
                             Description = LogData.Description,
                             PageName = LogData.PageName ?? "",
                             UserName = $"{Users.FirstName} {Users.LastName}",
                             CreatedOn = LogData.CreatedOn,
                             IpAddress = LogData.IpAddress ?? ""
                         }
                     ).AsQueryable();

                    int totalRecords = query.Count();

                    List<LogReportModel> result = new();

                    if (logReportRequestModel.RowPerPage > 0)
                        result = await query.Skip(logReportRequestModel.Offset).Take(logReportRequestModel.RowPerPage).ToListAsync();

                    if (result.Count > 0)
                        return new ResponseBaseModel(200, result, totalRecords);
                    else
                        return new ResponseBaseModel(404, new List<LogReportModel>(), "No record(s) found.");
                }
                else
                {
                    return new ResponseBaseModel(404, new List<LogReportModel>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetLoginReportAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    int[]? userChannel = null;

                    if (userRole != RoleEnum.Admin.ToString())
                        userChannel = await _userChannelService.GetUserChannelAsync(userId);

                    IQueryable<LoginReportModel> query =
                     (
                         from Users in _dbContext.Tbl_Users
                         where (userChannel == null || userChannel.Contains(Users.UserId))
                         orderby Users.LastLoginTime ascending

                         select new LoginReportModel
                         {
                             UserName = $"{Users.FirstName} {Users.LastName}",
                             LastLoginTime = Users.LastLoginTime,
                             LastLoginIp = Users.LastLoginIp ?? "",
                             LoginToday = Users.LastLoginTime == null ? false : Users.LastLoginTime.Value.Date == DateConverter.GetSystemDateTime().Date
                         }
                     ).AsQueryable();

                    List<LoginReportModel> result = await query.ToListAsync();

                    if (result.Count > 0)
                        return new ResponseBaseModel(200, result);
                    else
                        return new ResponseBaseModel(404, new List<LoginReportModel>(), "No record(s) found.");
                }
                else
                {
                    return new ResponseBaseModel(404, new List<LoginReportModel>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
