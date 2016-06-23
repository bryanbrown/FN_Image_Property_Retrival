using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using FileNet.Api.Core;
using FileNet.Api.Util;
using FileNet.Api.Collection;

namespace FN_Image_Property_Retrival
{
    class FN_GetFile
    {
        public void getFile(IObjectStore os, Id id, string path, string fileName)
        {
            IDocument doc = Factory.Document.FetchInstance(os, id, null);

            IContentElementList elemList = doc.ContentElements;

            foreach(IContentTransfer  ic in elemList)
            {
                string extension = ic.RetrievalName;

                try
                {
                    extension = extension.Substring(extension.LastIndexOf("."), 4);
                }
                catch
                {
                    extension = ".tif";
                }

                Stream stream = ic.AccessContentStream();

                double size = writeContent(stream, path + "\\" + fileName + extension);

                if (size != ic.ContentSize)
                    throw new IOException("Invalid content size retrieved for: " + fileName);
            }            
        }

        public double writeContent(Stream stream, string fileName)
        {
            byte[] buffer = new byte[4096];
            int bufferSize = 0;
            double size = 0;
            //string filePath = _path + "\\" + fileName;

            BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create));

            while((bufferSize = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                size += bufferSize;
                bw.Write(buffer, 0, bufferSize);
            }

            bw.Close();
            stream.Close();

            return size;
        }
    }
}
