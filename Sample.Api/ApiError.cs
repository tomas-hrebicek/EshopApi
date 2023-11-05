using System.Collections;
using System.Text.Json.Serialization;

namespace Sample.Api
{
    /// <summary>
    /// Represents api error.
    /// </summary>
    public sealed class ApiError
    {
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary Data { get; set; }
    }
}