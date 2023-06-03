using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUConWebHackathonProject.Models;
using System.ComponentModel;

namespace PUConWebHackathonProject.Controllers
{
    public class EventController : Controller
    {
        [Authorize]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]   
        public IActionResult CreateEvent(EventModel eventModel)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventModel.PosterPicture.FileName);
            var extension = Path.GetExtension(eventModel.PosterPicture.FileName);
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Posters", User.Identity.Name + extension);

            var newEvent = new EventModel
            {
                Id = Guid.NewGuid(),
                Title = eventModel.Title,
                Description = eventModel.Description,
                Date_Db = eventModel.Date.ToString(),
                Time_Db = eventModel.Time.ToString(),
                Duration = eventModel.Duration,
                PosterPicturePath = dbPath,
                Category= eventModel.Category,
            };


            return View();
        }
    }
}
