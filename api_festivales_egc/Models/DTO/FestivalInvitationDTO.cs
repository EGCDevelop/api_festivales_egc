namespace api_festivales_egc.Models.DTO
{
    public class FestivalInvitationDTO
    {
        public required int FestivalId { get; set; }

        public required string FestivalName { get; set; }

        public required string FestivalUbication { get; set; }

        public required DateTime FestivalDate { get; set; }

        public required TimeSpan FestivalStartTime { get; set; }

        public required int GestBandId { get; set; }

        public required string GestBandName { get; set; }

        public required byte GestBandState { get; set; }

        public required int GestFestivalId { get; set; }

        public required string GestFestivalInvitationKey { get; set; }

        public required byte GestFestivalActive { get; set; }

        public required int GestFestivalNumberEntries { get; set; }

        public required byte GestFestivalParticipation { get; set; }

    }
}
