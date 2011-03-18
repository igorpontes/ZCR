using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaRH
{
    /// <summary>
    /// Interface da classe Adaptador.
    /// </summary>
    interface IAdaptador
    {
        void AtualizarDocumento(Documento documento, string id);
        void InserirDocumento(Documento documento);
        void RemoverDocumento(int id);
        List<Documento> Todos();
        List<Documento> PorColuna(string select, string coluna, string orderBy);
        int EfetuaLogin(string nome, string senha);
        bool AlterarSenha(string nome, string novaSenha);
        //Documento obterProcessoPorId(string valor);
        List<Documento> PesquisarCampos(string comando);
        List<Arquivo> RetornaArquivos(List<Arquivo> lista);
    }
}
