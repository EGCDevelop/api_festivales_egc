namespace api_festivales_egc.Models
{
    public class Festival
    {
        public required int Id { get; set; }

		public required string Nombres { get; set; }

		public required int Anio { get; set; }

        public required string Ubicacion { get; set; }

		public required DateOnly Fecha { get; set; }

		public required TimeOnly HoraInicio { get; set; }

		public required TimeOnly HoraFin { get; set; }

        public required decimal PrecioEntrada { get; set; }
    }
}
