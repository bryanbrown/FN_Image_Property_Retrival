using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FN_Image_Property_Retrival
{
    class ScanDir
    {
        public string getOldestFile(string path)
        {
            foreach(var fi in new DirectoryInfo(path).GetFiles().OrderByDescending(x => x.LastWriteTime))
            {
                return fi.FullName;
            }

            return null;
        }
    }
}
