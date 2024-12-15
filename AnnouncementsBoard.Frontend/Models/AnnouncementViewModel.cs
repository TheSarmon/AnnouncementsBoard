namespace AnnouncementsBoard.Frontend.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> SubCategories { get; set; }
    }
}
