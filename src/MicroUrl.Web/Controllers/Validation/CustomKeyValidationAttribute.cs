namespace MicroUrl.Web.Controllers.Validation
{
    using System.ComponentModel.DataAnnotations;
    using MicroUrl.Web.Keys;

    public class CustomKeyValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var keyValidationService = (IKeyValidationService)validationContext.GetService(typeof(IKeyValidationService));
            if (value is null || (value is string key && keyValidationService.IsKeyValid(key)))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid custom key specified");
        }
    }
}