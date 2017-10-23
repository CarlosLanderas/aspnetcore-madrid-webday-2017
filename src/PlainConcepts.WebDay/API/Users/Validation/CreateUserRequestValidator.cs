using FluentValidation;
using PlainConcepts.WebDay.API.Users.Requests;
using PlainConcepts.WebDay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlainConcepts.WebDay.API.Users.Validation
{
    public class CreateUserRequestValidator: AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.Surname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.Roles)
                .NotEmpty().Must(BeAValidRole);         
            
        }

        private bool BeAValidRole(IEnumerable<string> roles)
        {
            return roles.All(r => r.Contains(r));
        }
    }
}
