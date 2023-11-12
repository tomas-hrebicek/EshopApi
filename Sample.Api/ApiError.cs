using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json.Serialization;

namespace Sample.Api
{
    /// <summary>
    /// Represents api error.
    /// </summary>
    public class ApiError
    {
        public static explicit operator ApiError(Exception exception)
        {
            return exception is null ? null : new ApiError()
            {
                Message = exception.Message,
                Data = exception.Data
            };
        }

        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary Data { get; set; }
    }

    /// <summary>
    /// Represents bad request api error.
    /// </summary>
    internal sealed class BadRequestApiError : ApiError
    {
        public BadRequestApiError(ActionContext actionContext)
            : base()
        {
            PrepareData(actionContext);
        }

        private void PrepareData(ActionContext actionContext)
        {
            if (actionContext is not null)
            {
                foreach (var state in actionContext.ModelState)
                {
                    this.Data.Add(state.Key, state.Value.Errors);
                }
            }
        }
    }
}