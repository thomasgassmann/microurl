namespace MicroUrl.Web.Controllers.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class DifferentHostAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;

            if (Uri.TryCreate(value?.ToString(), UriKind.Absolute, out Uri createdUri))
            {
                if (hostValue == createdUri.Authority)
                {
                    return new ValidationResult("Cannot shorten a link on the same host");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult($"{value?.ToString()} is not a valid URI");
        }
    }
}