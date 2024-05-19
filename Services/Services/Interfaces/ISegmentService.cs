using ViewModels.ViewModels;

namespace Services.Services
{
    public interface ISegmentService : IService
    {
        Task<ResponseBaseModel> GetPageLoadData();

        Task<ResponseBaseModel> GetAllSegment();
        
        Task<ResponseBaseModel> AddOrUpdateSegment(SegmentModel request);
        
        Task<ResponseBaseModel> DeleteSegment(int id);
    }
}
