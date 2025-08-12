using System.ComponentModel.DataAnnotations;

namespace Task_Progress_Generate_HTTP_Links.Models
{
    public class Url
    {
        public int Id { get; set; }

        [Required, MaxLength(2048)]
        public string OriginalUrl { get; set; }

        [Required, MaxLength(16)]
        public string ShortlUrl { get; set; }

        [Required, MaxLength(64)]
        public string SecretURL { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Visit> Visits { get; set; } = new();
    }
}
