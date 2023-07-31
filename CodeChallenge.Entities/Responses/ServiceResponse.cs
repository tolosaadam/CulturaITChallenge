using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace CodeChallenge.Entities.Responses
{
    public class ServiceResponse : BaseResponse
    {

        public ServiceResponse(bool success, string message, ValidationResult validationResult)
        {
            Success = success;
            Message = message;
            ValidationResult = validationResult;
        }

        public ServiceResponse(bool success) : this(success, string.Empty, null)
        { }

        public ServiceResponse(bool success, ValidationResult validationResult) : this(success, string.Empty, validationResult)
        { }

        public ServiceResponse(bool success, string message) : this(success, message, null)
        { }
    }
}
