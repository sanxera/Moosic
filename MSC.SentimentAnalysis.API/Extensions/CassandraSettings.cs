using Microsoft.Extensions.Configuration;

namespace MSC.SentimentAnalysis.API.Extensions
{
    public class CassandraSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
