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

namespace GED_TCESE
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ImageButtonCadastrar_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("cadastrar.aspx");
        }

        protected void ImageButtonListar_Click(object sender, ImageClickEventArgs e)
        {
            
            RadioButtonList rbl = (RadioButtonList)ContentPlaceHolder1.FindControl("RadioButtonListTipos");
            if (rbl.Items[0].Selected || rbl.Items[1].Selected || rbl.Items[2].Selected)
            {
                if (rbl.Items[0].Selected)
                {
                    Server.Transfer("listar.aspx");
                }
                else if (rbl.Items[1].Selected)
                {
                    Server.Transfer("listarprocessosdespesa.aspx");
                }
                else
                {
                    Server.Transfer("listarprotocolos.aspx");
                }
            }
            else
            {
                Response.Write("<script>alert('Informe o módulo que deseja listar')</script>");
            }
        }

        protected void ImageButtonAlterarSenha_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("alterarsenha.aspx");
        }

        protected void ImageButtonAjuda_Click(object sender, ImageClickEventArgs e)
        {
            
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
