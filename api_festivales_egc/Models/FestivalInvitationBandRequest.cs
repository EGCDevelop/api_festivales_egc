namespace api_festivales_egc.Models
{
    public class FestivalInvitationBandRequest
    {
        public string BandName { get; set; } = string.Empty;

        /// <summary>
        /// 1 = Invitada
        /// 2 = No invitada
        /// </summary>
        public byte State { get; set; }
    }
}
