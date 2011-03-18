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

namespace WSProntuario
{
    public class Tecnicos
    {
        public string matricula_Tecnico1 { get; set; }
        public string matricula_Tecnico2 { get; set; }
        public string matricula_Tecnico3 { get; set; }
        public string matricula_Tecnico4 { get; set; }
        public string nome_Tecnico1 { get; set; }
        public string nome_Tecnico2 { get; set; }
        public string nome_Tecnico3 { get; set; }
        public string nome_Tecnico4 { get; set; }
        public int qtdTecnicos { get; set; }
    }
}
