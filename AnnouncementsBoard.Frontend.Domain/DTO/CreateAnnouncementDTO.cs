// I did not use models from AnnouncementsBoard.Domain
// because it violates the independence of the microservices.
namespace AnnouncementsBoard.Frontend.Domain.DTO
{
    public class CreateAnnouncementDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
