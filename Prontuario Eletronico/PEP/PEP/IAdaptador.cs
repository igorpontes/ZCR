using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEP
{
    interface IAdaptador
    {
        void AtualizarProntuario(Prontuario prontuario);
        void InserirProntuario(Prontuario prontuario);
        void RemoverProntuario(int id);
        List<Prontuario> Todos();
        List<Prontuario> PorColuna(string coluna, string orderBy);
        int EfetuaLogin(string nome, string senha);
        bool AlterarSenha(string nome, string novaSenha);
        List<Prontuario> PesquisaPorCampo(string comando);
        Prontuario obterProntuarioPorId(string valor);
        List<Prontuario> PesquisarCampos(string comando);
    }
}
