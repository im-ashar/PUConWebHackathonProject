using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PUConWebHackathonProject.Models;
using PUConWebHackathonProject.Models.IRepositories;
using System.ComponentModel;
using System.IO;

namespace PUConWebHackathonProject.Controllers
{
    public class EventController : Controller
    {
        private IEventsRepository<EventModel> _eventsRepository;

        public EventController(IEventsRepository<EventModel> eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        [Authorize]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventModel eventModel)
        {
            var id = Guid.NewGuid();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventModel.PosterPicture.FileName);
            var extension = Path.GetExtension(eventModel.PosterPicture.FileName);
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Posters", id + extension);

            var newEvent = new EventModel
            {
                Id = id,
                Title = eventModel.Title,
                Description = eventModel.Description,
                Date_Db = eventModel.Date.ToString(),
                Time_Db = eventModel.Time.ToString(),
                Duration = eventModel.Duration,
                PosterPicturePath = dbPath,
                Category = eventModel.Category,
                CreatedBy = User.Identity.Name
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

            return RedirectToAction("EventsList");

        }
        [Authorize]
        public IActionResult EventsList()
        {
            var eventsList = _eventsRepository.GetAll();
            return View(eventsList);
        }

        public IActionResult EventsByUser()
        {
            var result = _eventsRepository.GetAll();
            result = result.Where(m => m.CreatedBy == User.Identity.Name).ToList();
            return View(result);
        }

        public IActionResult EditEvent(Guid eventId)
        {
            ViewBag.EventId = eventId;
            return View();
        }
        public IActionResult DeleteEvent(Guid eventId)
        {
            _eventsRepository.Delete(_eventsRepository.GetById(eventId));
            return RedirectToAction("EventsByUser");
        }

        [HttpPost]
        public IActionResult EditEvent(EventEditModel eventEditModel)
        {

            var model = _eventsRepository.GetById(eventEditModel.Id);

            if (eventEditModel.Title != null)
            {
                model.Title = eventEditModel.Title;
            }
            if (eventEditModel.Category != null)
            {
                model.Category = eventEditModel.Category;
            }
            if (eventEditModel.Date != null)
            {
                model.Date = eventEditModel.Date;
            }
            if (eventEditModel.Time != null)
            {
                model.Time = eventEditModel.Time;
            }
            if (eventEditModel.Description != null)
            {
                model.Description = eventEditModel.Description;
            }
            if (eventEditModel.Description != null)
            {
                model.Description = eventEditModel.Description;
            }
            if (eventEditModel.Duration != null)
            {
                model.Duration = eventEditModel.Duration;
            }
            if (eventEditModel.PosterPicture != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventEditModel.PosterPicture.FileName);
                var extension = Path.GetExtension(eventEditModel.PosterPicture.FileName);
                var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Posters", model.Id + extension);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    eventEditModel.PosterPicture.CopyTo(stream);
                }
                System.IO.File.Move(path, dbPath, true);
            }

            _eventsRepository.Update(model);
            return RedirectToAction("EventsByUser");


        }

    }
}
