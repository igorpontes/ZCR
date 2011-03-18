using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaRH
{
    interface IAdaptador
    {
        void AtualizarPessoa(Pessoa pessoa);
        void InserirPessoa(Pessoa pessoa);
        void RemoverPessoa(int id);
        List<Pessoa> Todos();
        List<Pessoa> PorColuna(string coluna);
        int EfetuaLogin(string nome, string senha);
        bool AlterarSenha(string nome, string novaSenha);
        List<Pessoa> PesquisaPorCampo(string comando);
        //Pessoa obterProcessoPorId(string valor);
        List<Pessoa> PesquisarCampos(string comando);
    }
}
