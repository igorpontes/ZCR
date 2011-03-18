using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GED_TCESE
{
    interface IAdaptadorDespesa
    {
        List<Despesa> Todos();
        List<Despesa> porColuna(string select, string coluna, string orderBy);
        List<Despesa> PesquisaPorCampo(string comando);
        Despesa obterDespesaPorId(string valor);
        List<Despesa> PesquisarCampos(string comando);
    }
}
