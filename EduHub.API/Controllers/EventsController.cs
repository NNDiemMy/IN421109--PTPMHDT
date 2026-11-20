using System;
using System.Collections.Generic;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventsController(EventService eventService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        /// <summary>
        /// Lấy danh sách sự kiện của CLB
        /// GET /api/clubs/{clubId}/events
        /// </summary>
        [HttpGet("api/clubs/{clubId:guid}/events")]
        [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetEventsByClub(Guid clubId)
        {
            if (clubId == Guid.Empty)
                return BadRequest(new { message = "Mã câu lạc bộ không hợp lệ." });

            var events = _eventService.GetByClubId(clubId);
            return Ok(events);
        }

        /// <summary>
        /// Xem một sự kiện theo Id
        /// GET /api/events/{id}
        /// </summary>
        [HttpGet("api/events/{id:guid}", Name = "GetEventById")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Mã sự kiện không hợp lệ." });

            var @event = _eventService.GetById(id);
            if (@event == null)
                return NotFound(new { message = $"Không tìm thấy sự kiện với Id: {id}" });

            return Ok(@event);
        }

        /// <summary>
        /// Tạo sự kiện mới cho CLB
        /// POST /api/clubs/{clubId}/events
        /// </summary>
        [HttpPost("api/clubs/{clubId:guid}/events")]
        [ProducesResponseType(typeof(Event), 201)]
        [ProducesResponseType(400)]
        public IActionResult CreateEvent(Guid clubId, [FromBody] CreateEventRequest request)
        {
            if (clubId == Guid.Empty)
                return BadRequest(new { message = "Mã câu lạc bộ không hợp lệ." });

            if (request == null)
                return BadRequest(new { message = "Dữ liệu yêu cầu không hợp lệ." });

            if (string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new { message = "Tiêu đề không được để trống." });

            if (request.Capacity <= 0)
                return BadRequest(new { message = "Sức chứa phải lớn hơn 0." });

            try
            {
                var eventDate = request.Date == default ? DateTime.UtcNow.AddDays(7) : request.Date;
                var channel = string.IsNullOrWhiteSpace(request.Channel) ? "email" : request.Channel.Trim();

                var createdEvent = _eventService.CreateEvent(
                    clubId,
                    request.Title,
                    eventDate,
                    request.Capacity,
                    channel
                );

                return CreatedAtRoute("GetEventById", new { id = createdEvent.Id }, createdEvent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xoá một sự kiện
        /// DELETE /api/events/{id}
        /// </summary>
        [HttpDelete("api/events/{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEvent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Mã sự kiện không hợp lệ." });

            var deleted = _eventService.DeleteEvent(id);
            if (!deleted)
                return NotFound(new { message = $"Không tìm thấy sự kiện với Id: {id} để xóa." });

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
