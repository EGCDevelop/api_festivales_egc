using System.Text;
using System.Text.Json.Serialization;

namespace api_festivales_egc.Models
{
    public class InvitationRequest
    {
        [JsonPropertyName("festivalId")]
        public int FestivalId { get; set; }

        [JsonPropertyName("festivalAccept")]
        public int FestivalAccept { get; set; }

        [JsonPropertyName("cancelDetail")]
        public string CancelDetail { get; set; } = string.Empty;

        // --- Property information
        [JsonPropertyName("establishmentName")]
        public string EstablishmentName { get; set; } = string.Empty;

        [JsonPropertyName("establishmentDirectorName")]
        public string EstablishmentDirectorName { get; set; } = string.Empty;

        [JsonPropertyName("establishmentEmail")]
        public string EstablishmentEmail { get; set; } = string.Empty;

        [JsonPropertyName("establishmentPhoneNumber")]
        public string EstablishmentPhoneNumber { get; set; } = string.Empty;

        // --- banda info
        [JsonPropertyName("bandStyle")]
        public string BandStyle { get; set; } = string.Empty;

        [JsonPropertyName("bandNumberMembers")]
        public int BandNumberMembers { get; set; }

        // --- leadership
        [JsonPropertyName("nameCommanderGeneral")]
        public string NameCommanderGeneral { get; set; } = string.Empty;

        [JsonPropertyName("nameSubComanderGeneral")]
        public string NameSubComanderGeneral { get; set; } = string.Empty;

        // --- instructor
        [JsonPropertyName("nameGeneralInstructor")]
        public string NameGeneralInstructor { get; set; } = string.Empty;

        [JsonPropertyName("phoneNumberGeneralInstructor")]
        public string PhoneNumberGeneralInstructor { get; set; } = string.Empty;

        [JsonPropertyName("emailGeneralInstructor")]
        public string EmailGeneralInstructor { get; set; } = string.Empty;

        // --- HISTORICAL OVERVIEW
        [JsonPropertyName("historicalOverview")]
        public string HistoricalOverview { get; set; } = string.Empty;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== DETALLES DE LA INVITACIÓN ===");
            sb.AppendLine($"FestivalId: {FestivalId}");
            sb.AppendLine($"FestivalAccept: {FestivalAccept}");
            sb.AppendLine($"Detalle de Cancelación: {CancelDetail}");
            sb.AppendLine("--- Información del Establecimiento ---");
            sb.AppendLine($"Nombre:                 {EstablishmentName}");
            sb.AppendLine($"Director:               {EstablishmentDirectorName}");
            sb.AppendLine($"Email:                  {EstablishmentEmail}");
            sb.AppendLine($"Teléfono:               {EstablishmentPhoneNumber}");
            sb.AppendLine("--- Información de la Banda ---");
            sb.AppendLine($"Estilo de Banda:        {BandStyle}");
            sb.AppendLine($"Número de Miembros:     {BandNumberMembers}");
            sb.AppendLine("--- Liderazgo ---");
            sb.AppendLine($"Comandante General:     {NameCommanderGeneral}");
            sb.AppendLine($"Subcomandante General:  {NameSubComanderGeneral}");
            sb.AppendLine("--- Instructor ---");
            sb.AppendLine($"Instructor General:     {NameGeneralInstructor}");
            sb.AppendLine($"Teléfono Instructor:    {PhoneNumberGeneralInstructor}");
            sb.AppendLine($"Email Instructor:       {EmailGeneralInstructor}");
            sb.AppendLine("--- Reseña Histórica ---");
            sb.AppendLine(string.IsNullOrWhiteSpace(HistoricalOverview) ? "[Vacía]" : HistoricalOverview);
            sb.AppendLine("=================================");

            return sb.ToString();
        }
    }
}
