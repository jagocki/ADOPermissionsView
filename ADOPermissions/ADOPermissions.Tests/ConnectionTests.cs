using ADOPermissions.Common;
using Microsoft.VisualStudio.Services.Graph.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ADOPermissions.Tests
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void ConnectionIsCreated()
        {
            string PAT = Environment.GetEnvironmentVariable("ADOConnection_PAT");
            string collectionUrl = Environment.GetEnvironmentVariable("ADOConnection_URL");
            ADOConnection aDOConnection = new ADOConnection(collectionUrl, PAT);
            using (GraphHttpClient graphClient = aDOConnection.Connection.GetClient<GraphHttpClient>())
            {
                PagedGraphGroups groups = graphClient.ListGroupsAsync().Result;
                Assert.IsNotNull(groups.GraphGroups);

            }
        }
    }
}
