using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using Logs;


namespace OracleDatabase
{
    class OracleDB
    {
        OracleConnection _conn = null;

        public OracleDB()
        {
            _conn = new OracleConnection(ConfigurationManager.ConnectionStrings["dbConnect"].ToString());
            _conn.Open();

            //Logging.log.LogInput(ConfigurationManager.ConnectionStrings["ICIP"].ToString());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_conn != null)
                {
                    _conn.Close();
                    _conn = null;
                }
            }
        }

        public OracleDataReader selectReader(string qry)
        {            
            OracleDataReader dr = null;

            try
            {
                OracleCommand cmd = new OracleCommand(qry, _conn);
                dr = cmd.ExecuteReader();

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Logging.log.LogInput(ex.StackTrace);
                Logging.log.LogInput(ex.Message);
            }
            
            return dr;
        }

        public int ExecuteNonQuery(string qry)
        {
            int rows = 0;

            try
            {
                OracleCommand cmd = new OracleCommand(qry, _conn);
                rows = cmd.ExecuteNonQuery();

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Logging.log.LogInput(ex.StackTrace);
                Logging.log.LogInput(ex.Message);
            }
            
            return rows;
        }

        public Decimal GetDocID()
        {
            Decimal OUT_Doc_Id = 0;


            try
            {
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["csFNPRD"].ToString());
                conn.Open();

                
                string err_msg = null;
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "FN_READ.FN_ADVCTR_UTILS_PKG.DOC_ID_ASSIGN";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter paramID = cmd.Parameters.Add("OUT_Doc_Id", OracleType.Number);
                paramID.Direction = ParameterDirection.Output;

                OracleParameter paramErr = cmd.Parameters.Add("OUT_Error_Msg", OracleType.VarChar);
                paramErr.Direction = ParameterDirection.Output;
                paramErr.Size = 200;

                cmd.ExecuteNonQuery();

                OUT_Doc_Id = (Decimal)paramID.Value;
                err_msg = paramErr.Value.ToString();

                cmd.Dispose();
                conn.Close();                
            }
            catch (Exception ex)
            {
                Logging.log.LogInput(ex.StackTrace);
                Logging.log.LogInput(ex.Message);
            }

            return OUT_Doc_Id;
        }

        public bool checkForBatch(string kofaxBatchID)
        {
            string qry = "SELECT * FROM ICIBTRACK WHERE KOFAXBATCHID = \'" + kofaxBatchID + "\'";

            OracleDataReader dr = selectReader(qry);

            bool bRows = false;

            if (dr != null)
            {
                bRows = dr.HasRows;
                dr.Close();
            }
            else
                bRows = false;


            return bRows;
        }
    }
}
