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
    public class Medicos
    {
        public string matricula_Medico1 { get; set; }
        public string matricula_Medico2 { get; set; }
        public string matricula_Medico3 { get; set; }
        public string matricula_Medico4 { get; set; }
        public string nome_Medico1 { get; set; }
        public string nome_Medico2 { get; set; }
        public string nome_Medico3 { get; set; }
        public string nome_Medico4 { get; set; }
        public int qtdMedicos { get; set; }
    }
}
