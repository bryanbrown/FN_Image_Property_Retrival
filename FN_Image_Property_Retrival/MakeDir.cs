using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
namespace FN_Image_Property_Retrival
{
    class MakeDir
    {
        public string path{get;}

        public string CreateDir(string path)
        {
            DirectoryInfo di = Directory.CreateDirectory(path);

            return di.FullName;
        }
    }
}
