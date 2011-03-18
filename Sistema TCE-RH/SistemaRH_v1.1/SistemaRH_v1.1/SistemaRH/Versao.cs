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
    /// Classe responsável pelo versionamento de um arquivo.
    /// </summary>
    public class Versao
    {
        public int? Id { get; set; }
        public string CaminhoDoArquivo { get; set; }
        public string NomeDoArquivo { get; set; }

        public string Extensao { get; set; }
    }
}
