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
using OracleDatabase;
using System.Data.OracleClient;

namespace FN_Image_Property_Retrival
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleDB db = new OracleDB();
            OracleDataReader odr = db.selectReader("select RO, DOC_ID, CONTRACT_TYPE FROM GENESIS.DME_REPORT WHERE LINE_TYPE = \'DOC\' ORDER BY RO");
            Check_If_File_Exists cie = new Check_If_File_Exists();
            bool fileExists = false;

            FN_Connection fnConn = new FN_Connection();
            FN_DocQry fnDocQry = new FN_DocQry();
            FN_GetFile fnFile = new FN_GetFile();
            MakeDir md = new MakeDir();
            Check_If_Dir_Exists cid = new Check_If_Dir_Exists();

            while (odr.Read())
            {
                string ro = odr.GetInt32(0).ToString();
                string docID = odr.GetInt32(1).ToString();
                string contractType = odr.GetString(2);

                string sharePath = GetDME.getSharePath(contractType);
                fileExists = cie.exists(ro, docID, sharePath);

                if (fileExists)
                    continue;

                cid.exists(sharePath + "\\" + ro); //check if dir exists, if not create it

                IRepositoryRowSet rowSet = fnDocQry.getRowSet(fnConn._os, "SELECT Id from [Orderdocs] WHERE (IsCurrentVersion = True) AND ROE =" + ro + " AND DocID =" + docID);

                foreach (IRepositoryRow row in rowSet)
                {
                    IProperties prop = row.Properties;

                    Id id = new Id(prop.GetObjectValue("Id").ToString());

                    fnFile.getFile(fnConn._os, id, sharePath + "\\" + ro, docID);
                }

            }

            //string path = ConfigurationManager.AppSettings["workingDir"].ToString();
            //string delimeter = ConfigurationManager.AppSettings["delimeter"];

            //ScanDir sd = new ScanDir();
            //string fileName = sd.getOldestFile(path);

            //DocID_List dl = new DocID_List(fileName, delimeter[0]);

            

           // foreach (var di in dl._docID_List)
            //{
            //    string roDir = md.CreateDir(path + "\\" + di._RO);

                
            //}

            //ZipFiles zf = new ZipFiles();
            //zf.zipDirectory(path, "c:\\temp\\dme.zip");
        }
    }
}


//check for docid files by ro folder in appropriate share
//if file is not present
//get file from filenet
//copy to ro folder

// mod config file for db
// copy in database class
// class to check for file exists
//select RO, DOC_ID, RO_CONTRACT_TYPE FROM GENESIS.DME_REPORT ORDER BY RO
