using System.Text.Json.Serialization;

namespace ViewModels.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeviceTypeEnum
    {
        UNKNOWN,
        DESKTOP,
        MOBILE,
        ANDROID,
        IOS,
    }
}
