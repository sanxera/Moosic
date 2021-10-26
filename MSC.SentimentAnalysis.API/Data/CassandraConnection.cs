using System;
using Cassandra;

namespace MSC.SentimentAnalysis.API.Data
{
    public class CassandraConnection
    {
        public static ISession OpenConnect()
        {
            var cluster = Cluster.Builder()
                .AddContactPoints("localhost")
                .WithPort(9042)
                .WithAuthProvider(new PlainTextAuthProvider("cassandra", "cassandra"))
                .Build();
            var session = cluster.Connect();


            return session;
//            var keyspaceNames = session
//                .Execute("SELECT * FROM system_schema.keyspaces")
//                .Select(row => row.GetValue<string>("keyspace_name"));
//​
//            Console.WriteLine("Found keyspaces:");
//            foreach (var name in keyspaceNames)
//            {
//                Console.WriteLine("- {0}", name);
//            }
        }
    }
}
