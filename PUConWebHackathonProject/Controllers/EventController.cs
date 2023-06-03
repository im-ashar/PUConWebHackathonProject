using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PUConWebHackathonProject.Models;
using PUConWebHackathonProject.Models.IRepositories;
using System.ComponentModel;
using System.IO;

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
        public async Task<IActionResult> CreateEvent(EventModel eventModel)
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
                Category = eventModel.Category,
            };
            var result = await _eventsRepository.Add(newEvent);
            if (result >= 1)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    eventModel.PosterPicture.CopyTo(stream);
                }
                System.IO.File.Move(path, dbPath, true);
            }

            return View();

        }
        public IActionResult EventsList()
        {
            ViewBag.EvetList = _ieventrepo.GetAll().ToList();
            return View();
        }
    }
}
