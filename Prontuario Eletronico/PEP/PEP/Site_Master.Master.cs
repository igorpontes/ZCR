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

namespace PEP
{
    public partial class Site_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButtonAjuda.Visible = false;
        }

        protected void ImageButtonCadastrar_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("cadastropaciente.aspx");
        }

        protected void ImageButtonListar_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("listar.aspx");
        }

        protected void ImageButtonAlterarSenha_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("alterarsenha.aspx");
        }

        protected void ImageButtonAjuda_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("ajuda.aspx");
        }

        protected void ImageButtonSair_Click(object sender, ImageClickEventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Server.Transfer("login.aspx");
        }

        protected void ImageButtonPrincipal_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("default.aspx");
        }
    }
}
