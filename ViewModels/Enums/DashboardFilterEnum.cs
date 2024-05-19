using System.Text.Json.Serialization;

namespace ViewModels.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DashboardFilterEnum
    {
        Today,
        Tomorrow,
        Pending
    }
}
