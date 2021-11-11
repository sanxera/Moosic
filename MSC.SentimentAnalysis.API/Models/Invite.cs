using System;

namespace MSC.SentimentAnalysis.API.Models
{
    public class Invite
    {
        public Guid Id { get; set; }
        public Guid IdEstablishment { get; set; }
        public Guid IdArtist { get; set; }
        public int ArtistRating { get; set; }
        public Comment ArtistComment { get; set; }
        public int EstablishmentRating { get; set; }
        public Comment EstablishmentComment{ get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
    }
}
