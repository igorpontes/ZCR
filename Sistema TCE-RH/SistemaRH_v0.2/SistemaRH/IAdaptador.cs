using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaRH
{
    interface IAdaptador
    {
        void AtualizarDocumento(Documento documento);
        void InserirDocumento(Documento documento);
        void RemoverDocumento(int id);
        List<Documento> Todos();
        List<Documento> PorColuna(string coluna);
        int EfetuaLogin(string nome, string senha);
        bool AlterarSenha(string nome, string novaSenha);
        List<Documento> PesquisaPorCampo(string comando);
        //Documento obterProcessoPorId(string valor);
        List<Documento> PesquisarCampos(string comando);
    }
}
