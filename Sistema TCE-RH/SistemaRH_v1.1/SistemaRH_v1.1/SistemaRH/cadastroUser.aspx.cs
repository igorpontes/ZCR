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
using LightInfocon.GoldenAccess.General;

namespace SistemaRH
{
    /// <summary>
    /// Classe responsável pelo cadastro de usuários do sistema.
    /// </summary>
    public partial class CadastroUser : System.Web.UI.Page
    {

        /// <summary>
        /// Método usado para cadastrar o usuário no banco.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonEnviar_Click(object sender, ImageClickEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.matricula = TextBoxMatricula.Text;
            usuario.login = TextBoxLogin.Text;
            usuario.password = TextSenha.Text;
            usuario.permissoes = retornaListaPermissoes();

            Adaptador adpt = new Adaptador();
            //inserir o usuario no GoldenAcess
            adpt.addUsuario(usuario);

            /***************** Faz criptografia da senha após cadastrar *********************/
            GoldenAccess goldenAccess = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            try
            {
                User usuarioAccess = goldenAccess.Authenticate(TextBoxLogin.Text, TextSenha.Text);
                GoldenAccessService servicoGoldenAccess = new GoldenAccessService(usuarioAccess);
                //servicoGoldenAccess.ChangePassword(usuario.login, Convert.ToBase64String(usuario.password));
                  //  ChangePassword(TextBoxLogin.Text, TextSenha.Text);
            }
            catch (Exception ex)
            {
                LabelErro.Text = ex.Message;
            }
        }

        /// <summary>
        /// Método usado para verificar se já existe o Login passado.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void CustomValidatorLogin_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //fazer acesso ao adaptador pra verificar se ja existe um login desse.
            Adaptador adpt = new Adaptador();
            if (adpt.existeLogin(args.Value))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        /// <summary>
        /// Método usado para verificar se já existe a matrícula passada.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void CustomValidatorMatricula_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Adaptador adpt = new Adaptador();
            if (!adpt.existeMatricula(args.Value))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        /// <summary>
        /// Método usado para retornar a lista de permissões do usuário a ser cadastrado.
        /// </summary>
        /// <returns>retorna uma lista de permissões</returns>
        protected List<Permissoes> retornaListaPermissoes()
        {
            Permissoes permissoes = new Permissoes();
            List<Permissoes> lista_permissoes = new List<Permissoes>();
            //Documentos Pessoais
            if (NenhumPessoais.Checked) { permissoes.opcao = "pessoais"; permissoes.tipo_permissao = 0; }
            else if (LeituraPessoais.Checked) { permissoes.opcao = "pessoais"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoPessoais.Checked) { permissoes.opcao = "pessoais"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);
            permissoes = new Permissoes();
            //Titulaçoes
            if (NenhumTitulacao.Checked) { permissoes.opcao = "titulacoes"; permissoes.tipo_permissao = 0; }
            else if (LeituraTitulacao.Checked) { permissoes.opcao = "titulacoes"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoTitulacao.Checked) { permissoes.opcao = "titulacoes"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);
            permissoes = new Permissoes();
            //CIs
            if (NenhumCI.Checked) { permissoes.opcao = "cis"; permissoes.tipo_permissao = 0; }
            else if (LeituraCI.Checked) { permissoes.opcao = "cis"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoCI.Checked) { permissoes.opcao = "cis"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);
            permissoes = new Permissoes();
            //Portarias
            if (NenhumPortaria.Checked) { permissoes.opcao = "portarias"; permissoes.tipo_permissao = 0; }
            else if (LeituraPortaria1.Checked) { permissoes.opcao = "portarias"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoPortaria.Checked) { permissoes.opcao = "portarias"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);
            permissoes = new Permissoes();
            //Portarias Com Processo
            if (NenhumPortariaProcesso.Checked) { permissoes.opcao = "portariasComProcesso"; permissoes.tipo_permissao = 0; }
            else if (LeituraPortariaProcesso.Checked) { permissoes.opcao = "portariasComProcesso"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoPortariaProcesso.Checked) { permissoes.opcao = "portariasComProcesso"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);
            permissoes = new Permissoes();
            //Aviso de Ferias
            if (NenhumAviso.Checked) { permissoes.opcao = "avisos"; permissoes.tipo_permissao = 0; }
            else if (LeituraAviso.Checked) { permissoes.opcao = "avisos"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoAviso.Checked) { permissoes.opcao = "avisos"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);
            permissoes = new Permissoes();
            //Requerimentos
            if (NenhumRequerimento.Checked) { permissoes.opcao = "requerimentos"; permissoes.tipo_permissao = 0; }
            else if (LeituraRequerimento.Checked) { permissoes.opcao = "requerimentos"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoRequerimento.Checked) { permissoes.opcao = "requerimentos"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);
            permissoes = new Permissoes();
            //Outros
            if (NenhumOutros.Checked) { permissoes.opcao = "outros"; permissoes.tipo_permissao = 0; }
            else if (LeituraOutros.Checked) { permissoes.opcao = "outros"; permissoes.tipo_permissao = 1; }
            else if (AlteracaoOutros.Checked) { permissoes.opcao = "outros"; permissoes.tipo_permissao = 2; }
            lista_permissoes.Add(permissoes);

            return lista_permissoes;
        }
    }
}
