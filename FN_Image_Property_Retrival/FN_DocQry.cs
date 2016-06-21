using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FileNet.Api.Collection;
using FileNet.Api.Core;
using FileNet.Api.Query;

namespace FN_Image_Property_Retrival
{
    class FN_DocQry
    {
        public IRepositoryRowSet getRowSet(IObjectStore os, string qry)
        {
            SearchSQL fnSQL = new SearchSQL(qry);
            IRepositoryRowSet rowSet = null;
            SearchScope scope = new SearchScope(os);

            rowSet = scope.FetchRows(fnSQL, null, null, true);

            return rowSet;
        }
    }
}
