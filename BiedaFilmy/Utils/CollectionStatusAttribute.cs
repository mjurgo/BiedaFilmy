using BiedaFilmy.Enums;
using BiedaFilmy.Models;
using System.ComponentModel.DataAnnotations;

namespace BiedaFilmy.Utils
{
    public class CollectionStatusAttribute : ValidationAttribute
    {
        public CollectionStatusAttribute(CollectionStatus status)
        => Status = status;

        public CollectionStatus Status { get; }

        public string GetErrorMessage() =>
            $"Ustaw prawidłowy status.";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var status = (CollectionStatus)value!;

            if (status == CollectionStatus.NotSeen)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
