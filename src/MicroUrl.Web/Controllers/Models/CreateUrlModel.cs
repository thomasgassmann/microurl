namespace MicroUrl.Web.Controllers.Models
{
    using System.ComponentModel.DataAnnotations;
    using MicroUrl.Web.Controllers.Validation;

    public class CreateUrlModel
    {
        [Required]
        [Url]
        [DifferentHost]
        public string Url { get; set; }

        [RegularExpression("[a-zA-Z0-9]{1,}")]
        [CustomKeyValidation]
        public string Key { get; set; }
    }
}