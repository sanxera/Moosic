using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSC.SentimentAnalysis.API.DTOs
{
    public class InvitesByLatitudeAndLongitudeDto
    {
        public Guid InviteId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
