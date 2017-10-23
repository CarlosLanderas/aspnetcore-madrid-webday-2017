using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PlainConcepts.WebDay.API.Users.Requests;

namespace PlainConcepts.WebDay.API.Users.Validation
{
    public class RemoveUserRequestValidator: AbstractValidator<RemoveUserRequest>
    {
        public RemoveUserRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
