using System;
using EduHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("api/clubs/{clubId}/events")]
        public IActionResult GetEventsByClub(Guid clubId)
        {
            var events = _eventService.GetByClubId(clubId);
            return Ok(events);
        }

        [HttpGet("api/events/{id}", Name = "GetEventById")]
        public IActionResult GetById(Guid id)
        {
            var evt = _eventService.GetById(id);
            if (evt == null)
            {
                return NotFound(new { message = "Không tìm thấy sự kiện" });
            }
            return Ok(evt);
        }

        [HttpPost("api/clubs/{clubId}/events")]
        public IActionResult CreateEvent(Guid clubId, [FromBody] CreateEventRequest request)
        {
            try
            {
                var evt = _eventService.CreateEvent(
                    clubId,
                    request.Title,
                    request.Date,
                    request.Capacity,
                    request.Channel
                );

                return CreatedAtRoute("GetEventById", new { id = evt.Id }, evt);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("api/events/{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            var success = _eventService.DeleteEvent(id);
            if (!success)
            {
                return NotFound(new { message = "Không tìm thấy sự kiện để xóa" });
            }
            return NoContent();
        }
    }

    public class CreateEventRequest
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public string Channel { get; set; } = "email";
    }
}
