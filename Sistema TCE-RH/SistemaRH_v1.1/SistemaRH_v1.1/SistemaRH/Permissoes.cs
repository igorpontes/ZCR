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
    /// <summary>
    /// Classe responsável pelo multivalorado de permissões da base de usuário
    /// </summary>
    public class Permissoes
    {
        public string opcao { get; set; }
        public int tipo_permissao { get; set; }
    }
}
