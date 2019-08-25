namespace MicroUrl.Controllers.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateUrlModel
    {
        [Required]
        [Url]
        public string Url { get; set; }
    }
}