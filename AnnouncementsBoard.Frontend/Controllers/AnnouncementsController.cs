using AnnouncementsBoard.Application.Services.Interfaces;
using AnnouncementsBoard.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using AnnouncementsBoard.Domain.Entities;

public class AnnouncementsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Dictionary<string, List<string>> _categories;

    public AnnouncementsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _categories = new Dictionary<string, List<string>>
            {
                { "Побутова техніка", new List<string> { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" } },
                { "Комп'ютерна техніка", new List<string> { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" } },
                { "Смартфони", new List<string> { "Android смартфони", "iOS/Apple смартфони" } },
                { "Інше", new List<string> { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" } }
            };
    }

    // GET: /Announcements
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("AnnouncementsAPI");
        var response = await client.GetAsync("announcements");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var announcements = JsonSerializer.Deserialize<List<Announcement>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(announcements);
    }

    // GET: /Announcements/Create
    public IActionResult Create()
    {
        ViewBag.Categories = _categories.Keys;
        ViewBag.SubCategories = new List<string>(); // Пустий список підкатегорій до вибору категорії
        return View(new Announcement());
    }

    // POST: /Announcements/Create
    [HttpPost]
    public async Task<IActionResult> Create(Announcement announcement)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _categories.Keys;
            ViewBag.SubCategories = _categories.ContainsKey(announcement.Category)
                ? _categories[announcement.Category]
                : new List<string>();
            return View(announcement);
        }

        var client = _httpClientFactory.CreateClient("AnnouncementsAPI");
        var json = JsonSerializer.Serialize(announcement);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("announcements", content);
        response.EnsureSuccessStatusCode();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Announcements/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var client = _httpClientFactory.CreateClient("AnnouncementsAPI");
        var response = await client.GetAsync($"announcements/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var announcement = JsonSerializer.Deserialize<Announcement>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ViewBag.Categories = _categories.Keys;
        ViewBag.SubCategories = _categories.ContainsKey(announcement.Category)
            ? _categories[announcement.Category]
            : new List<string>();

        return View(announcement);
    }

    // POST: /Announcements/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Announcement announcement)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _categories.Keys;
            ViewBag.SubCategories = _categories.ContainsKey(announcement.Category)
                ? _categories[announcement.Category]
                : new List<string>();
            return View(announcement);
        }

        var client = _httpClientFactory.CreateClient("AnnouncementsAPI");
        var json = JsonSerializer.Serialize(announcement);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"announcements/{id}", content);
        response.EnsureSuccessStatusCode();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Announcements/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient("AnnouncementsAPI");
        var response = await client.GetAsync($"announcements/{id}");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var announcement = JsonSerializer.Deserialize<Announcement>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(announcement);
    }

    // POST: /Announcements/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = _httpClientFactory.CreateClient("AnnouncementsAPI");
        var response = await client.DeleteAsync($"announcements/{id}");
        response.EnsureSuccessStatusCode();

        return RedirectToAction(nameof(Index));
    }
}
