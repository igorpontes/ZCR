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

namespace SistemaRH
{
    public partial class tab_default : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButtonVer.Visible = false;
            LinkButtonDelete.Visible = false;
        }

        protected void ButtonEnviar_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                //lembrar de adicionar o ID do cadastro ao nome na hora de adicionar ao banco
                String nameFile = "";
                if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPessoais"))
                {
                    nameFile = "docsPessoais.pdf";
                }
                else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelTitulacao"))
                {
                    nameFile = "titulacoes.pdf";
                }
                else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelPortaria"))
                {
                    nameFile = "portarias.pdf";
                }
                else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelCI"))
                {
                    nameFile = "cis.pdf";
                }
                else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelAviso"))
                {
                    nameFile = "avisoFerias.pdf";
                }
                else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelRequerimento"))
                {
                    nameFile = "requerimentos.pdf";
                }
                else if (Session["abaAtiva"].ToString().Equals((string)"TabPanelOutros"))
                {
                    nameFile = "outros.pdf";
                }
                
                //String nameFile = FileUpload1.FileName;
                try
                {
                    String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
                    Session.Add("pathDir", pathDir);
                    FileUpload1.SaveAs(pathDir + nameFile);
                    FileUpload1.SaveAs("C:\\Temp\\" + nameFile);
                    Session.Add("fileName", nameFile);
                    LabelArquivo.Text = nameFile;
                    LinkButtonVer.Visible = true;
                    LinkButtonDelete.Visible = true;
                }
                catch (Exception ex)
                {
                    LabelErro.Text = ex.Message;
                }
            }
            else
            {
                //Mostra o Erro quando não tem arquivo selecionado
                LabelErro.Text = "Selecione o arquivo";
            }


        }

        protected void LinkButtonVer_Click(object sender, EventArgs e)
        {
            //string nomeArquivo1 = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('\\') + 1);
            System.Diagnostics.Process.Start("C:\\Temp\\" + (String)Session["fileName"]);
            System.Diagnostics.Process.Start(Session["PathDir"].ToString());
            LinkButtonVer.Visible = true;
            LinkButtonDelete.Visible = true;

        }

        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            //String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
            File.Delete(Session["PathDir"].ToString() + (String)Session["fileName"]);
        }
    }
}