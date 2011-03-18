﻿using System;
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
    /// Classe contendo a definição do multivalorado Arquivos.
    /// </summary>
    public class Arquivo
    {
        public string nome_Arquivo { get; set; }
        public string conteudo_Arquivo { get; set; }
        public string tipo_Arquivo { get; set; }
        public int id_Arquivo { get; set; }
    }
}