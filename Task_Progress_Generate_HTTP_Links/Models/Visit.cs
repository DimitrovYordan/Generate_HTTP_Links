using System.ComponentModel.DataAnnotations;

namespace Task_Progress_Generate_HTTP_Links.Models
{
    public class Visit
    {
        public int Id { get; set; }

        public int UrlId { get; set; }
        public Url Url { get; set; }

        [Required, MaxLength(45)]
        public string IpAddress { get; set; }

        public DateTime VisitedAt { get; set; }
    }
}
