using System.Reflection.Metadata;

namespace WildlifeAPI.Models
{
    public class Services
    {
        public int id { get; set; }
        public string? serviceName { get; set; }
        public string? serviceDescription { get; set; }
        public string? location { get; set; }
        public string? phoneNumber { get; set; }
        public string? email { get; set; }
        public string? linkTo { get; set; }
        public string? image { get; set; }
    }
}
