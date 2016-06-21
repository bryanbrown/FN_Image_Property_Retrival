using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN_Image_Property_Retrival
{
    class RO_DocID
    {
        public string _RO { get; set; }
        public string _DocID { get; set; }

        public RO_DocID(string ro, string id)
        {
            _RO = ro;
            _DocID = id;
        }
    }
}
