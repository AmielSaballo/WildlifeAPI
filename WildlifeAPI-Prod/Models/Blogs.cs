namespace WildlifeAPI.Models
{
    public class Blogs
    {
        public int id { get; set; }
        public string? blogTitle { get; set; }

        public string? blogSummary { get; set; }
        public string? blogContent { get; set; }
        public string? blogAuthor { get; set; }
        public string? postedDate { get; set; }
        public string? blogImage { get; set; }
    }
}
