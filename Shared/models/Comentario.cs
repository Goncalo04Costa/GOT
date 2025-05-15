/*
*	<copyright file="Comentario" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 5/15/2025 12:47:47 PM</date>
*	<description></description>
**/

namespace Shared.models
{
    public class Comentario
    {
        #region ATRIBUTOS

        public int id;
        public string conteudo;
        public DateTime data;
        public int UtilizadorId { get; set; }
        public Utilizador? utilizador { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }

        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public Comentario()
        {

        }
        #endregion

        #region PROPRIEDADES

        #endregion

        #region OPERADORES

        #endregion

        #region OVERRIDES

        #endregion

        #region OUTROS METODOS

        #endregion

        #endregion
    }
}
