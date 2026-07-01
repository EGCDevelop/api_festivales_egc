namespace api_festivales_egc.Models
{
    public class FestivalInvitation
    {
		public required int Id { get; set; }

		public required string NombreBanda { get; set; }

        public required byte Estado { get; set; }

        public required int CantidadEntradas { get; set; }

        public required byte Participacion { get; set; }

        public required int FESId { get; set; }

        public required int BIFId { get; set; }
    }
}
