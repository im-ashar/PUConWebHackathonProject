using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUConWebHackathonProject.Models.Repositories.Identity
{
    public class IdentityModel : IdentityUser
    {
        [Required]
        [StringLength(20, ErrorMessage = "Length Should Not Exceed 15 Characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Length Should Not Exceed 15 Characters")]
        public string LastName { get; set; }

        [NotMapped]
        [Required]
        [StringLength(20, ErrorMessage = "Length Should Not Exceed 20 Characters")]
        public string User_Name { get; set; }

        [NotMapped]
        [Required]
        [EmailAddress]
        public string User_Email { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password Doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}
