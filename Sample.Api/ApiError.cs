using Microsoft.AspNetCore.Mvc;
using Sample.Application;
using System.Collections;
using System.Text.Json.Serialization;

namespace Sample.Api
{
    internal static class ErrorInfos
    {
        private static readonly List<ErrorInfoItem> _errorInfos = new List<ErrorInfoItem>()
        {
            new ErrorInfoItem(typeof(ForbiddenOperationError), "FORBIDDEN_OPERATION", "Operation is forbidden."),
            new ErrorInfoItem(typeof(AlreadyExistsError<>), "ITEM_ALREADY_EXISTS", "Item is already exists."),
            new ErrorInfoItem(typeof(NotFoundError), "ITEM_NOT_FOUND", "Item is not found.")
        };

        public static ErrorInfoItem Get(Error error)
        {
            ArgumentNullException.ThrowIfNull(error);
            return _errorInfos.FirstOrDefault(x => x.ErrorType == error.GetType());
        }
    }

    internal sealed class ErrorInfoItem
    {
        private readonly Type _errorType;
        private readonly string _code;
        private readonly string _description;

        public ErrorInfoItem(Type errorType, string code, string description)
        {
            _errorType = errorType;
            _code = code;
            _description = description;
        }

        public Type ErrorType => _errorType;
        public string Code => _code;
        public string Description => _description;
    }

    #region ApiError

    /// <summary>
    /// Represents api error.
    /// </summary>
    internal class ApiError
    {
        private const string API_ERROR_CODE_UNEXPECTED = "UNEXPECTED";

        private readonly string _code;

        protected ApiError(string code)
        {
            ArgumentException.ThrowIfNullOrEmpty(code);
            _code = code;
        }

        public static ApiError Unexpected()
        {
            return new ApiError(API_ERROR_CODE_UNEXPECTED)
            {
                Message = "Unexpected error"
            };
        }

        public static explicit operator ApiError(Exception exception)
        {
            if (exception is null)
            {
                return null;
            }
            else
            {
                var result = Unexpected();
                result.Message = exception.Message;
                result.Data = exception.Data;
                return result;
            }
        }

        public static explicit operator ApiError(Error error)
        {
            if (error is null)
            {
                return null;
            }

            var info = ErrorInfos.Get(error);

            var result = info is null ? Unexpected()
                : new ApiError(info.Code)
                {
                    Message = info.Description
                };

            result.Data = error;

            return result;
        }

        public string Code => _code;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary Data { get; set; } = new Dictionary<string, object>();
    }

    #endregion

    #region BadRequestApiError

    /// <summary>
    /// Represents bad request api error.
    /// </summary>
    internal sealed class BadRequestApiError : ApiError
    {
        private const string API_ERROR_CODE_BAD_REQUEST = "BAD_REQUEST";

        internal BadRequestApiError(ActionContext actionContext)
            : base(API_ERROR_CODE_BAD_REQUEST)
        {
            Message = "Parameters validation error.";

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

    #endregion
}