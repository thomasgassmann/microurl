namespace MicroUrl.Web.Controllers.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SignUpModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
