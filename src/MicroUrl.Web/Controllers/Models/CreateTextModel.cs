namespace MicroUrl.Web.Controllers.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTextModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string Language { get; set; }
    }
}