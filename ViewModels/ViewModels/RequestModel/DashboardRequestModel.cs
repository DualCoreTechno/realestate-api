using ViewModels.Enums;

namespace ViewModels.ViewModels
{
    public class DashboardRequestModel : BaseSearchModel
    {
        public DashboardFilterEnum NfdType { get; set; }
    }
}