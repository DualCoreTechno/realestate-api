using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IAreaService : IService
    {
        Task<ResponseBaseModel> GetAllAreas();

        Task<ResponseBaseModel> AddOrUpdateArea(MasterCommonFieldsModel request);

        Task<ResponseBaseModel> DeleteArea(int id);
    }
}
