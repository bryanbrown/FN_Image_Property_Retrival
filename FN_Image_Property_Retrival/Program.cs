using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FileNet.Api.Collection;
using FileNet.Api.Query;
using FileNet.Api.Property;
using FileNet.Api.Util;

using System.Configuration;

namespace FN_Image_Property_Retrival
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["workingDir"].ToString();
            string delimeter = ConfigurationManager.AppSettings["delimeter"];

            ScanDir sd = new ScanDir();
            string fileName = sd.getOldestFile(path);

            DocID_List dl = new DocID_List(fileName, delimeter[0]);

            FN_Connection fnConn = new FN_Connection();
            FN_DocQry fnDocQry = new FN_DocQry();
            FN_GetFile fnFile = new FN_GetFile(path);
            MakeDir md = new MakeDir();

            foreach (var di in dl._docID_List)
            {
                string roDir = md.CreateDir(path + "\\" + di._RO);

                IRepositoryRowSet rowSet = fnDocQry.getRowSet(fnConn._os, "SELECT Id from [Orderdocs] WHERE (IsCurrentVersion = True) AND ROE =" + di._RO + " AND DocID =" + di._DocID);

                foreach (IRepositoryRow row in rowSet)
                {
                    IProperties prop = row.Properties;

                    Id id = new Id(prop.GetObjectValue("Id").ToString());

                    fnFile.getFile(fnConn._os, id, roDir, di._DocID);
                }
            }

            ZipFiles zf = new ZipFiles();
            zf.zipDirectory(path, "c:\\temp\\dme.zip");
        }
    }
}
