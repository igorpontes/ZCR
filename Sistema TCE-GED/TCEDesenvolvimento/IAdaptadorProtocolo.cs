using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GED_TCESE
{
    interface IAdaptadorProtocolo
    {
        List<Protocolo> Todos();
        List<Protocolo> porColuna(string select, string coluna, string orderBy);
        List<Protocolo> PesquisaPorCampo(string comando);
        Protocolo obterProtocoloPorId(string valor);
        List<Protocolo> PesquisarCampos(string comando);
    }
}
