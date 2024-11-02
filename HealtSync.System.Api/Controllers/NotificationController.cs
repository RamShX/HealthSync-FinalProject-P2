using HealtSync.Domain.Entities.System;
using HealtSync.Persistence.Interfaces.System;
using HealtSync.Persistence.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace HealtSync.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsRepository _notificationsRepository;

        public NotificationsController(INotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id)
        {
            var result = await _notificationsRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var result = await _notificationsRepository.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notifications notification)
        {
            if (notification == null)
            {
                return BadRequest("La notificación no puede ser nula.");
            }

            var result = await _notificationsRepository.Save(notification);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetNotification), new { id = notification.NotificationID }, notification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] Notifications notification)
        {
            if (id != notification.NotificationID)
            {
                return BadRequest("El ID de la notificación no coincide.");
            }

            var result = await _notificationsRepository.Update(notification);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notificationToRemove = new Notifications { NotificationID = id };

            var result = await _notificationsRepository.Remove(notificationToRemove);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
