using AnnouncementsBoard.Frontend.Domain.Models;
using AnnouncementsBoard.Frontend.Domain.DTO;
using AnnouncementsBoard.Frontend.Application.Services.Interfaces;
using System.Net.Http.Json;

namespace AnnouncementsBoard.Frontend.Application.Services
{
    public class FrontendService : IFrontendService
    {
        private readonly HttpClient _httpClient;

        public FrontendService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("AnnouncementsAPI");
        }

        public async Task<List<Announcement>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Announcements");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<Announcement>>();
        }

        public async Task<Announcement> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Announcements/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Announcement>();
        }

        public async Task CreateAsync(CreateAnnouncementDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Announcements", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, UpdateAnnouncementDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"Announcements/{id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Announcements/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
