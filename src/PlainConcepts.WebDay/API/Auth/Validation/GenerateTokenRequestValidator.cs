using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PlainConcepts.WebDay.API.Auth.Requests;

namespace PlainConcepts.WebDay.API.Auth.Validation
{
    public class GenerateTokenRequestValidator: AbstractValidator<GenerateTokenRequest>
    {
        public GenerateTokenRequestValidator()
        {
            RuleFor(t => t.UserName).NotEmpty();
        }
    }
}
