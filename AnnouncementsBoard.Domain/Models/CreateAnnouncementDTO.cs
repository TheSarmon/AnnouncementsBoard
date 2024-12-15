namespace AnnouncementsBoard.Domain.Models
{
    public class CreateAnnouncementDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
