using Elastic.Elasticsearch.Xunit;

namespace EsIntegrationReprex
{
    /// <summary> Declare our cluster that we want to inject into our test classes </summary>
    public class MyTestCluster : XunitClusterBase
    {
        /// <summary>
        /// We pass our configuration instance to the base class.
        /// We only configure it to run version 6.2.3 here but lots of additional options are available.
        /// </summary>
        public MyTestCluster() : base(new XunitClusterConfiguration("6.2.0")) { }
    }
}