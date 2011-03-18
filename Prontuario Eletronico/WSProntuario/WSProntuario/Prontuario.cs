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

namespace WSProntuario
{
    public class Prontuario
    {
        public int id { get; set; }
        public string arq_Arquivo { get; set; }
        public string numero_Registro { get; set; }
        public string nome_Paciente { get; set; }
        public string naturalidade { get; set; }
        public DateTime data_Nascimento { get; set; }
        public string sexo { get; set; }
        public string nome_Pai { get; set; }
        public string nome_Mae { get; set; }
        public string profissao { get; set; }
        public string pessoa_Responsavel { get; set; }
        public Endereco endereco { get; set; }
        public Telefones telefone { get; set; }
        public string procedencia { get; set; }
        public string nome_Clinica_Diagnostico { get; set; }
        public string diagnostico { get; set; }
        public string cid { get; set; }
        public Medicos medico { get; set; }
        public Tecnicos tecnicos { get; set; }
        public string nome_Clinica_Internacao { get; set; }
        public string diagnostico_Provisorio { get; set; }
        public DateTime data_Internacao { get; set; }
        public string medico_Solicitante { get; set; }
    }
}