﻿using System;
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
using LightInfocon.GoldenAccess.General;

namespace SistemaRH
{
    /// <summary>
    /// Classe usada para edição da senha do usuário logado
    /// </summary>
    public partial class AlteraSenha : System.Web.UI.Page
    {
        int cont = 0;

        /// <summary>
        /// Metodo usado para autencicar o usuário.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxSenhaAtual.Focus();
            GoldenAccess goldenAccess;
            User usuario = new User("", ""); ;
            string nome = (String)Session["usuario"];
            string senha = (String)Session["senha"];
            try
            {
                goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
                usuario = goldenAccess.Authenticate(nome, senha);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
            if (usuario.IsAuthenticated)
            {
                LabelUsuario.Text = usuario.Login;
            }
            else
            {
                Server.Transfer("Login.aspx");
                Session.Abandon();
            }
        }

        /// <summary>
        /// Método usado para confirmar a alteração da senha verificando a senha antiga e cadastrando a nova.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonConfirmar_Click(object sender, ImageClickEventArgs e)
        {
            GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            try
            {
                User usuario = goldenAccess.Authenticate(LabelUsuario.Text, TextBoxSenhaAtual.Text);
                if (usuario.IsAuthenticated && cont <= 3)
                {
                    GoldenAccessService servicoGoldenAccess = new GoldenAccessService(usuario);
                    if (TextBoxNovaSenha.Text == TextBoxConfirmacaoSenha.Text)
                    {
                        cont++;
                        servicoGoldenAccess.ChangePassword(LabelUsuario.Text, TextBoxNovaSenha.Text);
                        LabelErro.Text = "Senha alterada com sucesso";
                    }
                    else
                    {
                        LabelErro.Text = "Senha e confirmação precissam ser iguais";
                    }
                }
                else
                {
                    LabelErro.Text = "Tentativas de mudança de senha excedidas";
                }
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
        }
    }
}
