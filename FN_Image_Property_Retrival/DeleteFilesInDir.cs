using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FN_Image_Property_Retrival
{
    class DeleteFilesInDir
    {
        public void deleteAll(string path)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(path);

                foreach (string fileName in filePaths)
                {
                    File.Delete(fileName);
                }
            }
            catch { }
        }
    }
}
