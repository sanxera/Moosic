using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSC.SentimentAnalysis.API.DTOs
{
    public class InvitesByEstablishmentsDto
    {
        public Guid InviteId { get; set; }
        public Guid EstablishmentId { get; set; }
    }
}
