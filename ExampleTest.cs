using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace EsIntegrationReprex
{
	public class ExampleTest : IClusterFixture<MyTestCluster>
	{
		public ExampleTest(MyTestCluster cluster)
		{
			// This registers a single client for the whole clusters lifetime to be reused and shared.
			// we do not expose Client on the passed cluster directly for two reasons
			//
			// 1) We do not want to prescribe how to new up the client
			//
			// 2) We do not want Elastic.Xunit to depend on NEST. Elastic.Xunit can start 2.x, 5.x and 6.x clusters
			//    and NEST Major.x is only tested and supported against Elasticsearch Major.x.
			//
			this.Client = cluster.GetOrAddClient(c =>
			{
				var nodes = cluster.NodesUris();
				var connectionPool = new StaticConnectionPool(nodes);
				var settings = new ConnectionSettings(connectionPool)
					.EnableDebugMode();
				return new ElasticClient(settings);
			});
		}

		private ElasticClient Client { get; }

		/// <summary> [I] marks an integration test (like [Fact] would for plain Xunit) </summary>
		[I]
		public void SomeTest()
		{
			var rootNodeInfo = this.Client.RootNodeInfo();

			rootNodeInfo.Name.Should().NotBeNullOrEmpty();
		}
	}
}
