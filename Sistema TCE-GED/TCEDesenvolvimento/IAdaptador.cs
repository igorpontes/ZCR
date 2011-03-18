using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GED_TCESE
{
    interface IAdaptador
    {
        void AtualizaProcesso(Processo processo);
        void InsereProcesso(Processo processo);
        void RemoveProcesso(int id);
        List<Processo> Todos();
        List<Processo> porColuna(string select, string coluna, string orderBy);
        int EfetuaLogin(string nome, string senha);
        bool AlterarSenha(string nome, string novaSenha);
        List<Processo> PesquisaPorCampo(string comando);
        Processo obterProcessoPorId(string valor);
        List<Processo> PesquisarCampos(string comando);
    }
}
