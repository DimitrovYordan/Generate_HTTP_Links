namespace Task_Progress_Generate_HTTP_Links.Models
{
    public class StatsViewModel
    {
        public string OriginalUrl { get; set; }

        public Dictionary<DateTime, int> UniqueVisitsPerDay { get; set; }

        public List<TopIdEntry> TopIds { get; set; }
    }
}
