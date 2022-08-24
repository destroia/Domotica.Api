using FluentValidation;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Validator
{
    public class LoginValidator : AbstractValidator<Login>
    {
        String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEqual("").NotEmpty().NotNull().Matches(expresion);
            RuleFor(x => x.Password).NotEqual("").NotEmpty().NotNull().MinimumLength(8);
        }
    }
}
