using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsManagement.Common.Helpers
{
    public class ValidDateAttribute : ValidationAttribute
    {
        static readonly DateTime MinValidDate = new DateTime(1900, 1, 1);

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;
            if (date <= DateTime.Today && date >= MinValidDate)
                return ValidationResult.Success;
            else
                return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage() =>
            "Требуется дата между 1.01.1900 и текущей";
    }
}
