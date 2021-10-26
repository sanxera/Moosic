using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;

namespace MSC.SentimentAnalysis.API.Data.Repositories
{
    public class Repository
    {
        public readonly ISession _session;

        public Repository(ISession session)
        {
            _session = CassandraConnection.OpenConnect();
        }
    }
}
