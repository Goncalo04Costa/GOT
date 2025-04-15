/*
*	<copyright file="TicketNotificacao" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 08/04/2025 14:42:09</date>
*	<description></description>
**/

using System.ComponentModel.DataAnnotations;

namespace Shared.models
{
    public enum EstadoTarefa
    {
        [Display(Name = "Todos")] Todos = -1,
        [Display(Name = "Por Iniciar")] PorIniciar = 0,
        [Display(Name = "Em Curso")] EmCurso = 1,
        [Display(Name = "Concluído")] Concluido = 2,
        [Display(Name = "Publicado")] Publicado = 3,
        [Display(Name = "Arquivadas")] Arquivado = 4,
        [Display(Name = "Em espera")] EmEspera = 5,
    }

    public class TicketNotificacao
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public EstadoTarefa Estado { get; set; }

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public int UtilizadorId { get; set; }
        public virtual Utilizador  Utilizador { get; set; }
        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public TicketNotificacao()
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
