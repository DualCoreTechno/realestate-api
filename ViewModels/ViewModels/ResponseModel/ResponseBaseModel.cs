using System.Text.Json.Serialization;

namespace ViewModels.ViewModels
{
    public class ResponseBaseModel
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; } = null;

        [JsonPropertyName("response")]
        public object? Response { get; set; } = null;

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; } = 0;

        public ResponseBaseModel(int statusCode, object responseBody, string responseMessage)
        {
            Message = responseMessage;
            Response = responseBody;
            Status = statusCode;
        }

        public ResponseBaseModel(int statusCode, string responseMessage)
        {
            Message = responseMessage;
            Status = statusCode;
        }

        public ResponseBaseModel(int statusCode, object responseBody, int totalCount = 0)
        {
            Response = responseBody;
            Status = statusCode;
            TotalCount = totalCount;
        }
    }
}
