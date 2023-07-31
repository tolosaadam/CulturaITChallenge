using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Entities.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ValidationResult ValidationResult {get;set;}
    }
}
