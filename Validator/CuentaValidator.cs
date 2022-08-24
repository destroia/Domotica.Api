using FluentValidation;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Validator
{
    public class CuentaValidator: AbstractValidator<Cuenta>
    {
        public CuentaValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().NotEqual("").Must(MachFromat);
            RuleFor(x => x.Celular).NotEmpty().NotEqual("").NotNull().MinimumLength(10);
            RuleFor(x => x.CuentaId).NotNull().NotEmpty();
            RuleFor(x => x.Pais).NotEmpty().NotNull().NotEqual("");
        }
        bool MachFromat(string email)
        {
            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return true;
            }
            return false;
        }
    }
}
