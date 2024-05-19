using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IMinMaxService : IService
    {
        Task<ResponseBaseModel> GetAllMinMax();
        
        Task<ResponseBaseModel> AddOrUpdateMinMax(MinMaxModel request);
        
        Task<ResponseBaseModel> DeleteMinMax(int id);
    }
}
