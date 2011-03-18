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

namespace SistemaRH
{
    public class Log
    {
        public int id { get; set; }
        public string usuario_log { get; set; }
        public string tipo_acao_log { get; set; }
        public string mensagem_acao_log { get; set; }
        public DateTime data_log { get; set; }
    }
}
