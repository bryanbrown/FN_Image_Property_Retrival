using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.IO;
using System.Net;

namespace FN_Image_Property_Retrival
{
    class FTP
    {
        private string _ftpServerIP;
        private string _ftpUserID;
        private string _ftpPassword;
        
        public FTP()
        {
            _ftpPassword = ConfigurationManager.AppSettings["ftpPass"];
            _ftpServerIP = ConfigurationManager.AppSettings["ftpServerIP"];
            _ftpUserID = ConfigurationManager.AppSettings["ftpUser"];
        }


        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public bool fileExists(string fileName, string dir)
        {
            string uri = "ftp://" + _ftpServerIP + "/images/" + dir + "/" + fileName + ".tif";
            var request = (FtpWebRequest)WebRequest.Create(uri);
            bool retVal = true;
            request.EnableSsl = true;
            request.UseBinary = true;          

            request.Credentials = new NetworkCredential(_ftpUserID, _ftpPassword);
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            try
            {
                FtpWebResponse resp = (FtpWebResponse)request.GetResponse();
            }
            catch(WebException ex)
            {
                FtpWebResponse exResp = (FtpWebResponse)ex.Response;

                if (exResp.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    retVal = false;
                else
                    throw (ex);
            }

            return retVal;
        }

        public bool uploadFile(string fileName, string ro)
        {
            FileInfo fi = new FileInfo(fileName);
            string uri = "ftp://" + _ftpServerIP + "/images/" + "/" + ro + "/" + fi.Name;
            FtpWebRequest ftpReq;

            ftpReq = (FtpWebRequest)FtpWebRequest.Create(uri);
            ftpReq.EnableSsl = true;
            ftpReq.Credentials = new NetworkCredential(_ftpUserID, _ftpPassword);
            ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
            ftpReq.ContentLength = fi.Length;

            int buffLen = 2048;
            byte[] buff = new byte[buffLen];
            int contentLen = 0;

            
            FileStream fs = fi.OpenRead();

            try
            {
                Stream str = ftpReq.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLen);

                while(contentLen != 0)
                {
                    str.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLen);
                }

                str.Close();
                fs.Close();    
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

		public bool CreateFTPDirectory(string directory)
		{

			try
			{
                //create the directory
                string uri = "ftp://" + _ftpServerIP + "/" + "images" + "/" + directory;
                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
				requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
				requestDir.Credentials = new NetworkCredential(_ftpUserID, _ftpPassword);
				requestDir.UsePassive = true;
				requestDir.UseBinary = true;
				requestDir.KeepAlive = false;
				FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
				Stream ftpStream = response.GetResponseStream();

				ftpStream.Close();
				response.Close();

				return true;
			}
			catch (WebException ex)
			{
				FtpWebResponse response = (FtpWebResponse)ex.Response;
				if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
				{
					response.Close();
					return true;
				}
				else
				{
					response.Close();
					return false;
				}
			}
		}
    }
}
