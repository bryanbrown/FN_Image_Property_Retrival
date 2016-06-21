using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Microsoft.Web.Services3.Security.Tokens;

using FileNet.Api.Util;
using FileNet.Api.Core;



namespace FN_Image_Property_Retrival
{
    
    class FN_Connection
    {
        public IObjectStore _os { get; }
        public FN_Connection(string objectStore)
        {
            string user = ConfigurationManager.AppSettings["CEUSER"].ToString();
            string pass = ConfigurationManager.AppSettings["CEPWD"].ToString();

            UsernameToken token = new UsernameToken(user, pass, PasswordOption.SendPlainText);

            UserContext.SetProcessSecurityToken(token);

            string url = ConfigurationManager.AppSettings["CEURL"].ToString();

            IConnection conn = null;
            conn = Factory.Connection.GetConnection(url);

            IDomain domain = null;
            domain = Factory.Domain.GetInstance(conn, null);

            _os = Factory.ObjectStore.FetchInstance(domain, objectStore, null);

        }

        public FN_Connection()
        {
            string user = ConfigurationManager.AppSettings["CEUSER"].ToString();
            string pass = ConfigurationManager.AppSettings["CEPWD"].ToString();

            UsernameToken token = new UsernameToken(user, pass, PasswordOption.SendPlainText);

            UserContext.SetProcessSecurityToken(token);

            string url = ConfigurationManager.AppSettings["FNCEURL"].ToString();

            IConnection conn = null;
            conn = Factory.Connection.GetConnection(url);

            IDomain domain = null;
            domain = Factory.Domain.GetInstance(conn, null);

            string objectStore = ConfigurationManager.AppSettings["CEOS"].ToString();

            _os = Factory.ObjectStore.FetchInstance(domain, objectStore, null);

        }
    }
}
