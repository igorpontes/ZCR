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
    public class Protocolo
    {
        public int id { get; set; }
        public string arq_Arquivo { get; set; }
        public string documento1 { get; set; }
        public string documento2 { get; set; }
        public string documento3 { get; set; }
        public string documento4 { get; set; }
        public string documento5 { get; set; }
        public string documento6 { get; set; }
    }
}
