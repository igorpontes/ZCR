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
using LightInfocon.GoldenAccess.General;
using System.Collections.Generic;
using System.IO;
using SistemaRH.Properties;

namespace SistemaRH
{
    /// <summary>
    /// Classe da master page usada no sistema.
    /// </summary>
    public partial class Site1 : System.Web.UI.MasterPage
    {
        string usuarioConectado;
        string senhaConectado;
        string erro;

        /// <summary>
        /// Método usado para verificar se o usuário esta autenticado.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuarioConectado = (String)Session["usuario"];
                senhaConectado = (String)Session["senha"];
                GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
                User usuario = new User(usuarioConectado, senhaConectado);
                try
                {
                    usuario = goldenAccess.Authenticate(usuarioConectado, senhaConectado);
                }
                catch (Exception ex)
                {
                    erro = ex.Message;
                }
                if (usuario.IsAuthenticated && !usuario.Disabled)
                {
                    if (usuario.HasGroup("RHADM"))
                    {
                        LinkButtonLog.Visible = true;
                    }
                    else if (usuario.HasGroup("RHLIM"))
                    {
                        LinkButtonLog.Visible = false;
                    }
                }
                else
                {
                    erro = "Usuário não autenticado";
                    Session.Add("erro", erro);
                    Server.Transfer("Login.aspx");
                    Session.Abandon();
                }
            }
        }

        /// <summary>
        /// Método usado para sair do sistema e abandona a sessão.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonSair_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Server.Transfer("Login.aspx");
        }

        /// <summary>
        /// Método usado para linkar o botão do LOG a sua respectiva página.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonLog_Click(object sender, EventArgs e)
        {
            Server.Transfer("log.aspx");
        }
    }
}
