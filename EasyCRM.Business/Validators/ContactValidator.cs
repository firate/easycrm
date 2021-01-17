using EasyCRM.Entity.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Validators
{
    public class ContactValidator:AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(c=>c.LastName).NotEmpty().MaximumLength(50);
            RuleFor(c => c.AccountId).NotEmpty().WithMessage("Every contact must be connected with an account!");
            RuleFor(c => c.AccountId).GreaterThan(0).WithMessage("AccountId must be greater than zero(0).");
        }
    }
}
