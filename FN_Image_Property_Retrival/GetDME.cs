using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace FN_Image_Property_Retrival
{
    static class GetDME
    {
        static public string getSharePath(string code)
        {
            //in future pass code for argument, also put codes in config file
            return ConfigurationManager.AppSettings["sunftp"];
        }
    }
}
