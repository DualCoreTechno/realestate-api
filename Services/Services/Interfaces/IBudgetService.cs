using ViewModels.ViewModels;

namespace Services.Services
{
    public interface IBudgetService : IService
    {
        Task<ResponseBaseModel> GetPageLoadData();

        Task<ResponseBaseModel> GetAllBudget();
        
        Task<ResponseBaseModel> AddOrUpdateBudget(BudgetModel request);
        
        Task<ResponseBaseModel> DeleteBudget(int id);
    }
}
