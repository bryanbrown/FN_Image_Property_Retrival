using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FN_Image_Property_Retrival
{
    class Check_If_File_Exists
    {
        public bool exists(string ro, string docId, string sharePath)
        {
            string path = sharePath + "\\" + ro;
            string filePattern = docId + ".*";
            bool rtn = false;
            string[] files = null;

            try
            {
                files = System.IO.Directory.GetFiles(path, filePattern, System.IO.SearchOption.TopDirectoryOnly);
            }
            catch { }

            if (files != null && files.Length > 0)
            {
                rtn = true;
            }

            return rtn;
        }
    }
}
