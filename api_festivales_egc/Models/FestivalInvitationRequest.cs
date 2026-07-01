namespace api_festivales_egc.Models
{
    public class FestivalInvitationRequest
    {
        public string ClaveInvitacion { get; set; } = string.Empty;

        /// <summary>
        /// 1 = Activa
        /// 0 = Inactiva
        /// </summary>
        public byte Activa { get; set; }

        public short CantidadEntradas { get; set; }

        /// <summary>
        /// 1 = Participa
        /// 0 = No participa
        /// </summary>
        public byte Participacion { get; set; }

        public int FESId { get; set; }

        public int BIFId { get; set; }
    }
}
