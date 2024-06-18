using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiAuth.Models
{
    public class TblUser
    {
        [Key]  public int ID { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Designation { get; set; }

        [DefaultValue("default_value")]
        public string UserMessage { get; set; }
        [DefaultValue("default_value")]
        public string AccessToken { get; set; }
    }
}
