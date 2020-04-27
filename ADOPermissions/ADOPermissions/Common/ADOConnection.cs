using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADOPermissions.Common
{
    public class ADOConnection
    {
        public VssConnection Connection
        {
            get;
        }

        public string PAT
        {
            get;
        }

        public string CollectionUrl
        {
            get;
        }

        public ADOConnection(string collectionUrl, string PAT)
        {
            this.PAT = PAT;
            this.CollectionUrl = collectionUrl;
            Connection = SetUpConnection(collectionUrl, PAT);
        }


        private VssConnection SetUpConnection(string collectionUrl, string PAT)
        {
           return new VssConnection(new Uri(collectionUrl), new VssBasicCredential(PAT, PAT));
        }
    }
}
