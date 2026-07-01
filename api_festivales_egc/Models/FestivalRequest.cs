namespace api_festivales_egc.Models
{
    public class FestivalRequest
    {
        public string Name { get; set; } = string.Empty;

        public short Year { get; set; }
        
        public string Ubication { get; set; } = string.Empty;
        
        public DateTime Date { get; set; }
        
        public TimeSpan StartDate { get; set; }
        
        public TimeSpan EndDate { get; set; }
        
        public decimal EntrancePrice { get; set; }
    }
}
