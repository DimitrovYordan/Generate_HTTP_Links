using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Progress_Generate_HTTP_Links.Models
{
    public class Visit
    {
        public int Id { get; set; }

        [ForeignKey("Url")]
        public int UrlId { get; set; }
        public Url Url { get; set; }

        [Required, MaxLength(45)]
        public string IpAddress { get; set; }

        public DateTime VisitedAt { get; set; } = DateTime.UtcNow;
    }
}
