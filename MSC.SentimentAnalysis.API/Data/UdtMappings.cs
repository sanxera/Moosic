using Cassandra;
using MSC.SentimentAnalysis.API.Models;

namespace MSC.SentimentAnalysis.API.Data
{
    public static class UdtMappings
    {
        public static void DefineMappings(this ISession session)
        {
            session.ChangeKeyspace("invites_dc");
            session.UserDefinedTypes.Define(UdtMap.For<Comment>());
        }
    }
}
