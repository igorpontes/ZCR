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
    public class Telefone
    {
        public string numero_TelefoneFixo { get; set; }
        public string numero_TelefoneCelular { get; set; }
    }
}
