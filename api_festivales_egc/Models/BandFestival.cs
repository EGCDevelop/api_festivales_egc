namespace api_festivales_egc.Models
{
    public class BandFestival
    {
        public required int Id { get; set; }

		public required string NombreBanda { get; set; }

		public required byte Estado { get; set; }
    }
}
