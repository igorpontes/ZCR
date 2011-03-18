using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SistemaRH
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Adaptador adpt = new Adaptador();
            List<Arquivo> lista = new List<Arquivo>();
            for (int i = 0; i < 3; i++)
            {
                Arquivo arq = new Arquivo();
                arq.nome_Arquivo = "identidade" + i + ".pdf";
                lista.Add(arq);
            }
            lista = adpt.RetornaArquivos(lista);
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }
    }
}
