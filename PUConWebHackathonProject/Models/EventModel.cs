using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUConWebHackathonProject.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title Is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Date Is Required")]
        [DataType(DataType.Date)]
        public DateOnly? Date { get; set; }
        public string Date_Db { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Time Is Required")]
        [DataType(DataType.Time)]
        public TimeOnly? Time { get; set; }
        public string Time_Db { get; set; }

        [Required(ErrorMessage = "Duration Is Required")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Category Is Required")]
        public string Category { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Poster Picture Is Required")]
        [DataType(DataType.Upload)]
        public IFormFile PosterPicture { get; set; }
        public string PosterPicturePath { get; set; }
        public string CreatedBy { get; set; }
    }
}
