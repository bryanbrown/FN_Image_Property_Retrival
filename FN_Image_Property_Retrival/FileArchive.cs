using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FN_Image_Property_Retrival
{
    class FileArchive
    {
        public void archiveFile(string fileName, string archiveDirPath)
        {
            string destFile = archiveDirPath + "\\" + fileName;

            File.Move(fileName, destFile);
        }
    }
}
