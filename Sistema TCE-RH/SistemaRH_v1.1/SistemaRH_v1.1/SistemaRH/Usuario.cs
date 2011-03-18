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
    /// <summary>
    /// Classe responsável pela definição da base de usuário.
    /// </summary>
    public class Usuario
    {
        public string matricula { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public List<Permissoes> permissoes { get; set; }
    }
}
