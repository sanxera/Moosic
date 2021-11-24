using Cassandra;
using Cassandra.Mapping;
using MSC.SentimentAnalysis.API.Extensions;

namespace MSC.SentimentAnalysis.API.Data.Repositories
{
    public class Repository
    {
        public readonly IMapper _mapper;
        public readonly ISession _session;
        private readonly CassandraSettings _cassandraSettings;

        public Repository(CassandraSettings cassandraSettings)
        {
            _cassandraSettings = cassandraSettings;
            _session = OpenConnect();
            _session.DefineMappings();
            _mapper = new Mapper(_session);
        }

        private ISession OpenConnect()
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
