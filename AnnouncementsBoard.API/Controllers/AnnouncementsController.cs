using AnnouncementsBoard.Application.Services;
using AnnouncementsBoard.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // GET: api/announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAll()
        {
            var announcements = await _announcementService.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        // GET: api/announcements/category/{category}
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetByCategory(string category)
        {
            var announcements = await _announcementService.GetAnnouncementsByCategoryAsync(category);
            if (announcements == null || !announcements.Any())
            {
                return NotFound($"No announcements found in the category '{category}'.");
            }
            return Ok(announcements);
        }

        // GET: api/announcements/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetById(int id)
        {
            var announcement = await _announcementService.GetAnnouncementByIdAsync(id);
            if (announcement == null)
            {
                return NotFound($"Announcement with ID {id} not found.");
            }
            return Ok(announcement);
        }

        // POST: api/announcements
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Повертаємо помилки валідації
            }

            await _announcementService.AddAnnouncementAsync(announcement);
            return CreatedAtAction(nameof(GetById), new { id = announcement.Id }, announcement);
        }

        // PUT: api/announcements/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Announcement announcement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Перевірка валідності
            }

            if (id != announcement.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the body.");
            }

            try
            {
                await _announcementService.UpdateAnnouncementAsync(announcement);
                return NoContent(); // Успішне оновлення
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Якщо оголошення з таким ID не знайдено
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/announcements/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _announcementService.DeleteAnnouncementAsync(id);
                return NoContent(); // Успішне видалення
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Якщо оголошення з таким ID не знайдено
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
