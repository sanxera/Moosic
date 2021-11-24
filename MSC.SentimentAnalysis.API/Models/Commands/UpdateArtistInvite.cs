using System;

namespace MSC.SentimentAnalysis.API.Models.Commands
{
    public class UpdateArtistInvite
    {
        public Guid Id { get; set; }
        public int ArtistRating { get; set; }
        public Comment ArtistComment { get; set; }
    }
}
