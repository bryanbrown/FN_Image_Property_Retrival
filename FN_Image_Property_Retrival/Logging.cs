using System;
using System.Configuration;
using System.IO;


namespace Logs
{
    public class Logging
    {
        public class log : Logging
        {
            public static void LogInput(string strComment)
            {
                string _strComment = strComment;

                object oDirectory = ConfigurationManager.AppSettings["Logs"].ToString();

                string strInfoFileNet = ConfigurationManager.AppSettings["Logs"].ToString() + "\\" +
                    String.Format("{0:yyyyMMdd}", System.DateTime.Now) + ".log";

                if (!System.IO.Directory.Exists(oDirectory.ToString()))
                {
                    System.IO.Directory.CreateDirectory(oDirectory.ToString());
                }

                FileStream fsInfoFileNet = new FileStream(strInfoFileNet, FileMode.Append, FileAccess.Write);
                StreamWriter fsFileWriter = new StreamWriter(fsInfoFileNet);

                try
                {
                    fsFileWriter.Write(String.Format("{0:yyyyMMdd hh:mm:ss}", System.DateTime.Now) + " " + _strComment + "");
                    fsFileWriter.Write(Environment.NewLine);
                }
                catch (Exception err)
                {
                    Logging.logError.GetError(err.ToString());
                    Logging.logError.GetError(err.StackTrace);
                    //SendMail._SendMail(err.ToString());
                }
                finally
                {
                    _strComment = null;
                    fsFileWriter.Flush();
                    fsFileWriter.Close();

                }
            }

        }
        public class logError : Logging
        {
            public static void GetError(string err)
            {
                FileStream Fs = null;

                try
                {
                    string _err = err;

                    object oDirectory = ConfigurationManager.AppSettings["Logs"].ToString();

                    if (!System.IO.Directory.Exists(oDirectory.ToString()))
                    {
                        System.IO.Directory.CreateDirectory(oDirectory.ToString());
                    }

                    string ErrDate = System.DateTime.Today.ToString();

                    string ErrFile = ConfigurationManager.AppSettings["Logs"].ToString() + "\\" + String.Format("{0:yyyyMMdd}", System.DateTime.Now) + "FileDocument.err";



                    Fs = new FileStream(ErrFile, FileMode.Append, FileAccess.Write);
                    StreamWriter FsWriter = new StreamWriter(Fs);
                    FsWriter.WriteLine("************************************" + String.Format("{0:yyyyMMdd hh:mm:ss}", System.DateTime.Now) + "************************************");
                    FsWriter.WriteLine(_err.ToString() + "/n");
                    FsWriter.WriteLine("********************************************************************************************" + "/n");
                    FsWriter.Close();
                }
                finally
                {
                    if(Fs != null)
                        Fs.Close();
                }
               
            }
        }
    }
}