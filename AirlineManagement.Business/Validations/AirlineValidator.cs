using AirlineManagement.Business.DTOs.AirlineDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Business.Validations
{
    public class AirlineValidator : AbstractValidator<AirlineCreateDto>
    {
        public AirlineValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Airline name is required.");
            RuleFor(a => a.Country).NotEmpty().WithMessage("Country is required.");

        }
    }
}
