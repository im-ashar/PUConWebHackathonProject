using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PUConWebHackathonProject.Models.Repositories
{
    public class LoginModel
    {
        [NotMapped]
        [Required]
        [StringLength(20, ErrorMessage = "Length Should Not Exceed 20 Characters")]
        public string User_Name { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
