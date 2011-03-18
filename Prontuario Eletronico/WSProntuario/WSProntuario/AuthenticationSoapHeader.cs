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
using System.Web.Services.Protocols;

namespace WSProntuario
{
    public class AuthenticationSoapHeader : SoapHeader
    {
        private string _devToken;
        private string _matricula_Medico;
        private string _numero_Registro;

        public string Matricula_Medico
        {
            get { return _matricula_Medico; }
            set { _matricula_Medico = value; }
        }

        public string Numero_Registro
        {
            get { return _numero_Registro; }
            set { _numero_Registro = value; }
        }

        public string DevToken
        {
            get { return _devToken; }
            set { _devToken = value; }
        }

        public AuthenticationSoapHeader() { }

        public AuthenticationSoapHeader(string devToken, string matricula_Medico, string numero_Registro)
        {
            _devToken = devToken;
            _matricula_Medico = matricula_Medico;
            _numero_Registro = numero_Registro;
        }
    }
}
