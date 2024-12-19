using AnnouncementsBoard.Frontend.Application.Services.Interfaces;
using AnnouncementsBoard.Frontend.Domain.DTO;
using AnnouncementsBoard.Frontend.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AnnouncementsBoard.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFrontendService _service;
        private readonly IMapper _mapper;

        public HomeController(IFrontendService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string category, string subcategory, string searchQuery)
        {
            var announcements = await _service.GetFilteredAsync(category, subcategory, searchQuery);

            ViewBag.Categories = Categories.CategoryData.Keys.ToList();
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
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _service.GetByIdAsync(id);
            if (announcement == null) return NotFound();

            var dto = _mapper.Map<UpdateAnnouncementDTO>(announcement);

            ViewBag.Categories = Categories.CategoryData.Keys.ToList();
            ViewBag.SubCategories = Categories.CategoryData[dto.Category];
            ViewBag.Statuses = Statuses.StatusData;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateAnnouncementDTO dto)
        {
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
