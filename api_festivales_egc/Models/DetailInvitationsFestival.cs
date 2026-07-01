namespace api_festivales_egc.Models
{
    public class DetailInvitationsFestival
    {
        public required int Id { get; set; }

        public string? Correo { get; set; }
        
        public string? NombreEstablecimiento { get; set; }
        
        public string? NombreDirector { get; set; }

        public string? CorreoEstablecimiento { get; set; }

        public string? NumeroEstablecimiento { get; set; }

        public string? EstiloBanda { get; set; }

        public string? NombreComandanteGeneral { get; set; }

        public string? NombreSubcomandanteGeneral { get; set; }

        public int? CantidadIntegrantes { get; set; } = 0;

        public string? NombreInstructorBanda { get; set; }

        public string? NumeroInstructorBanda { get; set; }

        public string? CorreoInstructorBanda { get; set; }

        public string? ReseniaHistorica { get; set; }

        public required int IFId { get; set; }

        public string? ComentarioRechazo { get; set; }
    }
}
