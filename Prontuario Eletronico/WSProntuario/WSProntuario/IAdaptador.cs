using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSProntuario
{
    interface IAdaptador
    {
        List<Prontuario> obterProntuarioPorRegistro(AuthenticationSoapHeader authentication);
        List<Prontuario> consultarEquipe(AuthenticationSoapHeader authentication);
        List<Prontuario> listarProntuarios(AuthenticationSoapHeader authentication);
        bool validaAcesso(AuthenticationSoapHeader authentication);
    }
}
