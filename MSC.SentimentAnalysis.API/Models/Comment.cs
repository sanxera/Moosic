using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSC.SentimentAnalysis.API.Models
{
    public class Comment
    {
        public string Content { get; set; }
        public int Feeling { get; set; }
    }
}
