using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUConWebHackathonProject.Models
{
    public class EventEditModel
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }


        [DataType(DataType.Date)]
        public DateOnly? Date { get; set; }


        [DataType(DataType.Time)]
        public TimeOnly? Time { get; set; }

        public string? Duration { get; set; }

        public string? Category { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? PosterPicture { get; set; }
        public string? CreatedBy { get; set; }
    }
}
