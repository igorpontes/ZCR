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
using System.Collections.Generic;

namespace SistemaRH
{
    public class Documento
    {
        public int id { get; set; }
        public string matricula_Colaborador { get; set; }
        public string nome_Colaborador { get; set; }
        public string cpf_Colaborador { get; set; }
        public string foto{ get; set; }
        public List<Arquivo> arquivos { get; set; }
    }
}
