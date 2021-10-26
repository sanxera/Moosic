using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSC.SentimentAnalysis.API.DTOs
{
    public class InvitesByArtists
    {
        public Guid InviteId { get; set; }
        public Guid ArtistId { get; set; }
    }
}
