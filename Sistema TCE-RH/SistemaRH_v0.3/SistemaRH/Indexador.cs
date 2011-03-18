using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistemaRH.Properties;
using LightInfocon.GoldenIndex.General;

namespace SistemaRH
{
    public class Indexador
    {
        public void Indexe(Versao versao)
        {
            try
            {

                IGoldenIndex goldenIndex =
                    GoldenIndexClient.Instance(Settings.Default.MaquinaGoldenIndex,
                                               Settings.Default.PortaGoldenIndex,
                                               Settings.Default.UriGoldenIndex,
                                               Settings.Default.ProtocoloGoldenIndex);
                User usuarioGoldenIndex = GoldenIndexClient.Authenticate(Settings.Default.UsuarioGoldenIndex,
                                                                         Settings.Default.SenhaGoldenIndex, goldenIndex);

                string extensao = versao.Extensao.StartsWith(".") ? versao.Extensao : string.Concat(".", versao.Extensao);
                if (!goldenIndex.IsSupported(usuarioGoldenIndex, extensao))
                {
                    // Se não for um arquivo suportado, não faz nada
                    return;
                }

                FileData arquivo = new FileData();
                arquivo.Url = Settings.Default.CaminhoDoRepositorioDocumento + versao.NomeDoArquivo;
                arquivo.IndexerParameters = new IndexerParameters
                {
                    Table = "documento",
                    ContentField = "conteudo_Arquivo",
                    IdField = "id",
                    IdFieldValue = versao.Id.ToString()
                };
                goldenIndex.SaveFile(usuarioGoldenIndex, arquivo);
            }
            catch (Exception exception)
            {
                string erro = exception.Message;
            }
        }
    }
}
