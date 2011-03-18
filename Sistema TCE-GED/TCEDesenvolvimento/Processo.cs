using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace GED_TCESE
{
    public class Processo
    {
        public int id { get; set; }
        public string arq_Arquivo { get; set; }
        public string decisao { get; set; }
        public string numero_Processo { get; set; }
        public string ano_Processo { get; set; }
        public string origem { get; set; }
        public string assunto { get; set; }
        public string descricao { get; set; }
        public int qtdPessoas { get; set; }
        public string pessoa1 { get; set; }
        public string pessoa2 { get; set; }
        public string pessoa3 { get; set; }
        public string pessoa4 { get; set; }
    }
}
