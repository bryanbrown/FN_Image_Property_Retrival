using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FN_Image_Property_Retrival
{
    class Check_If_Dir_Exists
    {
        public void exists(string path)
        {
            if (!Directory.Exists(path))
            {
                MakeDir md = new MakeDir();
                md.CreateDir(path);
            }
        }
    }
}
