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
using System.IO;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.IO;


namespace SistemaRH
{
    public partial class tab_titulacoes : System.Web.UI.UserControl
    {
        private int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButtonVer.Visible = false;
            LinkButtonDelete.Visible = false;
            Session.Add("count", 0);
            Session.Add("nomeArquivoOriginal", null);
        }

        protected void ButtonEnviar_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                String nameFile = FileUpload1.FileName;
                try
                {
                    String pathDir = HttpContext.Current.Server.MapPath("~/arquivos/");
                    FileUpload1.SaveAs(pathDir + nameFile);
                    FileUpload1.SaveAs("C:\\Temp\\" + nameFile);
                    Session.Add("fileName", nameFile);

                    //pega do contador a quantidade de arquivos que foram adicionados pra poder deixar o nome do arquivo principal
                    count = (int)Session["count"];
                    Session.Add("count", count++);
                    if (count == 1)
                    {
                        Session.Add("nomeArquivoOriginal", nameFile);
                    }
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
            System.Diagnostics.Process.Start("C:\\Temp\\"  +(String)Session["fileName"]);
            LinkButtonVer.Visible = true;
            LinkButtonDelete.Visible = true;

        }

        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            File.Delete(@"~\arquivos\" + (String)Session["fileName"]);
        }


    }
}