using System;

namespace MSC.SentimentAnalysis.API.Models.Commands
{
    public class UpdateEstablishmentInvite
    {
        public Guid Id { get; set; }
        public int EstablishmentRating { get; set; }
        public Comment EstablishmentComment { get; set; }
    }
}
