using System;
using Cassandra;
using MSC.SentimentAnalysis.API.Extensions;

namespace MSC.SentimentAnalysis.API.Data
{
    public class CassandraConnection
    {
        private readonly CassandraSettings _cassandraSettings;

        private CassandraConnection(CassandraSettings cassandraSettings)
        {
            _cassandraSettings = cassandraSettings;
        }

        public ISession OpenConnect()
        {
            var cluster = Cluster.Builder()
                .AddContactPoints(_cassandraSettings.Host)
                .WithPort(_cassandraSettings.Port)
                .WithAuthProvider(new PlainTextAuthProvider(_cassandraSettings.User, _cassandraSettings.Password))
                .Build();
            var session = cluster.Connect();

            return session;
        }
    }
}
