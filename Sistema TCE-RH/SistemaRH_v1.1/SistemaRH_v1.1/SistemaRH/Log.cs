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
using System.Collections.Generic;

namespace SistemaRH
{
    /// <summary>
    /// Classe responsável pela estrutura do LOG da base.
    /// </summary>
    /// <remarks>Implementa a interface IComparable.</remarks>
    public class Log : IComparable<Log>
    {
        public int id { get; set; }
        public string usuario_log { get; set; }
        public string tipo_acao_log { get; set; }
        public string mensagem_acao_log { get; set; }
        public DateTime data_log { get; set; }

        #region IComparable<Log> Members
        /// <summary>
        /// Método usado para comparar dois Logs.
        /// </summary>
        /// <param name="other">Outro LOG.</param>
        /// <returns>Um inteiro de comparação</returns>
        public int CompareTo(Log other)
        {
            return data_log.CompareTo(other.data_log);
        }

        /// <summary>
        /// Método estático usado para comprar duas datas de dois LOGs.
        /// </summary>
        public static Comparison<Log> DataComparison = delegate(Log log1, Log log2)
        {
            return log1.data_log.CompareTo(log2.data_log);
        };

        /// <summary>
        /// Método estático usado para comprar dois usuários de dois LOGs.
        /// </summary>
        public static Comparison<Log> UsuarioComparison = delegate(Log log1, Log log2)
        {
            return log1.usuario_log.CompareTo(log2.usuario_log);
        };

        /// <summary>
        /// Método estático usado para comprar dois tipos de ação de dois LOGs.
        /// </summary>
        public static Comparison<Log> TipoAcaoComparison = delegate(Log log1, Log log2)
        {
            return log1.tipo_acao_log.CompareTo(log2.tipo_acao_log);
        };

        /// <summary>
        /// Método estático usado para comprar duas mensagens de dois LOGs.
        /// </summary>
        public static Comparison<Log> MensagemComparison = delegate(Log log1, Log log2)
        {
            return log1.mensagem_acao_log.CompareTo(log2.mensagem_acao_log);
        };


       
        #endregion
    }
}
