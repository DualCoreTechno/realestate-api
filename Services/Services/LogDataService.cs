using Data.Data;
using Data.Entity;
using Services.Settings;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class LogDataService : ILogDataService
    {
        private readonly AppDB _dbContext;
        
        public LogDataService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseBaseModel> AddLogData(LogDataModel request)
        {
            try
            {
                var logs = new Tbl_LogData
                {
                    Description = request.Description,
                    Module = request.Module,
                    PageName = request.PageName,
                    IpAddress = request.IpAddress,
                    CreatedBy = request.UserId,
                    CreatedOn = DateConverter.GetSystemDateTime()
                };

                _dbContext.Tbl_LogData.Add(logs);
                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Done.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}