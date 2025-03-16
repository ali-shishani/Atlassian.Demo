using Atlassian.Demo.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Atlassian.Demo.Models
{
    public class ApiResponseBuilder<T>
    {
        private readonly ApiResponse<T> _apiResponse;

        public ApiResponse<T> Build() => _apiResponse;

        public ApiResponseBuilder()
        {
            _apiResponse = new ApiResponse<T>();
        }

        public ApiResponseBuilder<T> WithData(T data)
        {
            _apiResponse.Data = data;
            return this;
        }

        public ApiResponseBuilder<T> WithError(Error error)
        {
            if (_apiResponse.Errors == null)
            {
                _apiResponse.Errors = new List<Error>();
            }

            _apiResponse.Errors.Add(error);

            return this;
        }

        public ApiResponseBuilder<T> WithErrors(IEnumerable<Error> errors)
        {
            if (_apiResponse.Errors == null)
            {
                _apiResponse.Errors = new List<Error>();
            }

            foreach (var error in errors)
            {
                _apiResponse.Errors.Add(error);
            }

            return this;
        }

        public ApiResponseBuilder<T> WithHttpStatus(Microsoft.AspNetCore.Http.HttpResponse response, System.Net.HttpStatusCode statusCode)
        {
            response.StatusCode = (int)statusCode;
            return this;
        }

        public ApiResponseBuilder<T> With404NotFoundError(Microsoft.AspNetCore.Http.HttpResponse response)
        {
            return WithError(new Error
            {
                Code = ErrorConstants.PermissionDeniedCode,
                Description = ErrorConstants.PermissionDeniedDescription,
                Message = ErrorConstants.PermissionDeniedMessage
            }).WithHttpStatus(response, System.Net.HttpStatusCode.NotFound);
        }
    }
    public class ApiResponseBuilder
    {
        private readonly ApiResponse _apiResponse;

        public ApiResponse Build() => _apiResponse;

        public ApiResponseBuilder()
        {
            _apiResponse = new ApiResponse();
        }

        public ApiResponseBuilder WithError(Error error)
        {
            if (_apiResponse.Errors == null)
            {
                _apiResponse.Errors = new List<Error>();
            }

            _apiResponse.Errors.Add(error);

            return this;
        }

        public ApiResponseBuilder WithErrors(IEnumerable<Error> errors)
        {
            if (_apiResponse.Errors == null)
            {
                _apiResponse.Errors = new List<Error>();
            }

            foreach (var error in errors)
            {
                _apiResponse.Errors.Add(error);
            }

            return this;
        }

        public ApiResponseBuilder WithHttpStatus(Microsoft.AspNetCore.Http.HttpResponse response, System.Net.HttpStatusCode statusCode)
        {
            response.StatusCode = (int)statusCode;
            return this;
        }

        public ApiResponseBuilder With404NotFoundError(Microsoft.AspNetCore.Http.HttpResponse response)
        {
            return WithError(new Error
            {
                Code = ErrorConstants.PermissionDeniedCode,
                Description = ErrorConstants.PermissionDeniedDescription,
                Message = ErrorConstants.PermissionDeniedMessage
            }).WithHttpStatus(response, System.Net.HttpStatusCode.NotFound);
        }
    }
}
