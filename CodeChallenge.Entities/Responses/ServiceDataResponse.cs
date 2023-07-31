using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Entities.Responses
{
    public class ServiceDataResponse<T> : BaseResponse where T : class
    {
        public T Data { get; private set; }
       
        public ServiceDataResponse(bool success, string message, T data, ValidationResult validationResult)
        {
            this.Data = data;
            Success = success;
            Message = message;
            ValidationResult = validationResult;
        }

        public ServiceDataResponse(bool success, T data) : this(success, string.Empty, data, null)
        { }

        public ServiceDataResponse(bool success, T data, ValidationResult validationResult) : this(success, string.Empty, data, validationResult)
        { }

        public ServiceDataResponse(bool success, string message) : this(success, message, null, null)
        { }

        public ServiceDataResponse(bool success, string message, ValidationResult validationResult) : this(success, message, null, validationResult)
        { }
    }
}
