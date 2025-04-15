/*
*	<copyright file="Tarefas" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 08/04/2025 14:48:26</date>
*	<description></description>
**/

namespace Shared.models
{
    public class Tarefas
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public EstadoTarefa EstadoTarefa { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataFim {  get; set; }
        public string TipoTarefa { get; set; } 

        public int EquipaId { get; set; }
        public virtual Equipas Equipas { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public Tarefas()
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
