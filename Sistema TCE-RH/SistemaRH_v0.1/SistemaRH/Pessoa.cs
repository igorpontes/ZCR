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

namespace SistemaRH
{
    public class Pessoa
    {
        public int id { get; set; }
        public string arq_Arquivo { get; set; }
        public string nome_Colaborador { get; set; }
        public string naturalidade { get; set; }
        public DateTime data_Nascimento { get; set; }
        public char sexo { get; set; }
        public string nome_Pai { get; set; }
        public string nome_Mae { get; set; }
        public string cargo { get; set; }
        public Endereco endereco { get; set; }
        public Telefone telefone { get; set; }
     
    }
}