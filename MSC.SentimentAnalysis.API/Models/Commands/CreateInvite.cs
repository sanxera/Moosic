using System;

namespace MSC.SentimentAnalysis.API.Models.Commands
{
    public class CreateInvite
    {
        public Guid Id { get; set; }
        public Guid IdEstablishment { get; set; }
        public Guid IdArtist { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
    }
}
