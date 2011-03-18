using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.IO;
using System.IO;
using System.Collections.Generic;

namespace SistemaRH
{
    public partial class tab_editar : System.Web.UI.UserControl
    {
        String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
        Adaptador adpt = new Adaptador();
        List<Arquivo> lista = new List<Arquivo>();
        string diretorio;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carrega_Grid("docsPessoais");
                carrega_Grid("titulacoes");
                carrega_Grid("portarias");
                carrega_Grid("cis");
                carrega_Grid("avisoFerias");
                carrega_Grid("requerimentos");
                carrega_Grid("outros");
                lista = adpt.RetornaArquivos(lista);
                Session.Add("carregou_grid", "nao");
            }
        }

        protected void carrega_Grid(string opcao)
        {
            lista = (List<Arquivo>)Session[opcao];
            lista = adpt.RetornaArquivos(lista);
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}