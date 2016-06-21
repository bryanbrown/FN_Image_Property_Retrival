using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Configuration;


namespace FN_Image_Property_Retrival
{
    class DocID_List
    {
        private char _delimeter;
        private int _docIDIndex, _ROIndex;
        public List<RO_DocID> _docID_List { get; } 
        public DocID_List(string fileName, char delim)
        {
            _docID_List = new List<RO_DocID>();
            _delimeter = delim;
            _docIDIndex = Int32.Parse(ConfigurationManager.AppSettings["DocID_ZeroBasedIndex"]);
            _ROIndex = Int32.Parse(ConfigurationManager.AppSettings["RO_ZeroBasedIndex"]);

            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = File.OpenText(fileName))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                    getDocID(sb.ToString());
                    sb.Clear();
                }
                    
            }
            
            
        }

        private void getDocID(string param)
        {            
            string[] fields = param.Split(_delimeter);
            ///////////////
            //this will change
            if (fields[1].Trim().ToUpper() != "DOCS")
                return;

            string ro = fields[_docIDIndex];
            string id = fields[_ROIndex];
            //end configurables
            _docID_List.Add(new RO_DocID(ro, id));
        }
    }
}
