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
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.IO;
using System.IO;
using LightInfocon.GoldenAccess.General;

namespace PEP
{
    public partial class MergeDocuments : System.Web.UI.Page
    {

        string erro;

        protected void Page_Load(object sender, EventArgs e)
        {
            //// Create a merge document and set it's properties
            //MergeDocument document = MergeDocument.Merge( MapPath( "../PDFs/DocumentA.pdf" ), MapPath( "../PDFs/DocumentB.pdf" ));

            //// Append additional PDF
            //document.Append( MapPath( "../PDFs/DocumentC.pdf" ) );
            //// Append 3 pages from an aditional PDF
            //document.Append( MapPath( "../PDFs/DocumentD.pdf" ), 1, 3 );

            //// Outputs the merged document to the current web page
            //document.DrawToWeb( "MergePDFs.pdf" );
            string usuarioConectado = (String)Session["usuario"];
            string senhaConectado = (String)Session["senha"];
            GoldenAccess servicoDeAutenticacao = new GoldenAccess("http://localhost:3271/GoldenAccess.soap");
            LightInfocon.GoldenAccess.General.User usuarioGoldenAccess = new LightInfocon.GoldenAccess.General.User(usuarioConectado, senhaConectado);
            try
            {
                usuarioGoldenAccess = servicoDeAutenticacao.Authenticate(usuarioConectado, senhaConectado);
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            if (usuarioGoldenAccess.IsAuthenticated)
            {
                FileUpload1.Focus();
            }
            else
            {
                //Caso não seja o mesmo usuário, este será redirecionado para o login e a sessão será limpa.
                erro = "Usuário não autenticado";
                Session.Add("erro", erro);
                Session.Abandon();
                Server.Transfer("login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Create a merge document and set it's properties
            MergeDocument document = MergeDocument.Merge(FileUpload1.PostedFile.FileName, FileUpload2.PostedFile.FileName);

            string nomeArquivo1 = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('\\')+1);

            // Outputs the merged document to the current web page
            //document.DrawToWeb(FileUpload1.PostedFile.FileName);
            document.Draw(nomeArquivo1);

            // Opens the PDF (Requires a PDF Viewer)
            //System.Diagnostics.Process.Start(nomeArquivo1);

            // Put the file on a specific Path
            System.IO.File.Move(nomeArquivo1, "c://Temp//" + nomeArquivo1);

        }
    }
}
