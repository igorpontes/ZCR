using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;

namespace WSProntuario
{
    /// <summary>
    /// Summary description for ServiceProntuario1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    public class ServiceProntuario1 : System.Web.Services.WebService
    {
        Authentication authentication = new Authentication();
        private const string DEV_TOKEN = "12345";

        public ServiceProntuario1()
        {
            
        }

        private bool validaChave(AuthenticationSoapHeader authentication)
        {
            Adaptador adpt = new Adaptador();
            if (adpt.validaAcesso(authentication))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validaChaveMedico(AuthenticationSoapHeader authentication)
        {
            Adaptador adpt = new Adaptador();
            if (adpt.validaAcessoMedico(authentication))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [SoapHeader("authentication")]
        [WebMethod]
        public bool login(AuthenticationSoapHeader authentication)
        {
            if (authentication != null && authentication.DevToken == DEV_TOKEN)
            {
                if (validaChave(authentication))
                {
                    return true;
                }
                else
                {
                    return false; ;
                }
            }
            else
            {
                throw new Exception("Falha de Autenticação");
            }
        }

        [SoapHeader("authentication")]
        [WebMethod]
        public List<Prontuario> consultaProntuario(AuthenticationSoapHeader authentication)
        {
            if (validaChave(authentication))
            {
                Adaptador adpt = new Adaptador();
                List<Prontuario> prontuario = new List<Prontuario>();
                prontuario = adpt.obterProntuarioPorRegistro(authentication);
                return prontuario;
            }
            else
            {
                return null;
            }
        }

        [SoapHeader("authentication")]
        [WebMethod]
        public List<Prontuario> consultaEquipe(AuthenticationSoapHeader authentication)
        {
            if (validaChave(authentication))
            {
                List<Prontuario> lista = new List<Prontuario>();
                Adaptador adpt = new Adaptador();
                lista = adpt.consultarEquipe(authentication);
                return lista;
            }
            else
            {
                return null;
            }
        }

        [SoapHeader("authentication")]
        [WebMethod]
        public List<Prontuario> listarProntuarios(AuthenticationSoapHeader authentication)
        {
            if (validaChaveMedico(authentication))
            {
                List<Prontuario> lista = new List<Prontuario>();
                Adaptador adpt = new Adaptador();
                lista = adpt.listarProntuarios(authentication);
                return lista;
            }
            else
            {
                return null;
            }
        }
    }
}