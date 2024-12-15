using AnnouncementsBoard.Application.Services.Interfaces;
using AnnouncementsBoard.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementsController(IAnnouncementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await _service.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            var announcement = await _service.GetAnnouncementByIdAsync(id);
            if (announcement == null)
                return NotFound();

            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDTO createAnnouncementDTO)
        {
            if (createAnnouncementDTO == null)
            {
                return BadRequest("Announcement data is required.");
            }

            var createdAnnouncement = await _service.CreateAnnouncementAsync(createAnnouncementDTO);

            return CreatedAtAction(nameof(GetAnnouncementById), new { id = createdAnnouncement.Id }, createdAnnouncement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] UpdateAnnouncementDTO updateAnnouncementDTO)
        {
            if (updateAnnouncementDTO == null)
            {
                return BadRequest("Announcement data is required.");
            }

            var updatedAnnouncement = await _service.UpdateAnnouncementAsync(id, updateAnnouncementDTO);

            return Ok(updatedAnnouncement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await _service.DeleteAnnouncementAsync(id);
            return NoContent();
        }
    }
}