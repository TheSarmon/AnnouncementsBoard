using AnnouncementsBoard.Frontend.Application.Services.Interfaces;
using AnnouncementsBoard.Frontend.Domain.DTO;
using AnnouncementsBoard.Frontend.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsBoard.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFrontendService _service;

        public HomeController(IFrontendService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _service.GetAllAsync();
            return View(announcements);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = Categories.CategoryData.Keys.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = Categories.CategoryData.Keys.ToList();
                return View(dto);
            }

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _service.GetByIdAsync(id);
            if (announcement == null) return NotFound();

            var dto = new UpdateAnnouncementDTO
            {
                Title = announcement.Title,
                Description = announcement.Description,
                Category = announcement.Category,
                SubCategory = announcement.SubCategory,
                Status = announcement.Status
            };

            ViewBag.Categories = Categories.CategoryData.Keys.ToList();
            ViewBag.Statuses = new List<string> { "Активне", "Неактивне" }; // Додаємо статуси
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateAnnouncementDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = Categories.CategoryData.Keys.ToList();
                ViewBag.Statuses = new List<string> { "Активне", "Неактивне" }; // Додаємо статуси
                return View(dto);
            }

            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
