namespace MicroUrl.Controllers.Models
{
    using System.ComponentModel.DataAnnotations;
    using MicroUrl.Controllers.Validation;

    public class CreateUrlModel
    {
        [Required]
        [Url]
        [DifferentHostAttribute]
        public string Url { get; set; }
    }
}