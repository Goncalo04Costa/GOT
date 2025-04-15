/*
*	<copyright file="Equipas" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 08/04/2025 14:38:34</date>
*	<description></description>
**/

namespace Shared.models
{
    public class Equipas
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public ICollection<UtilizadorEquipa> UtilizadorEquipas { get; set; } = new List<UtilizadorEquipa>();
        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public Equipas()
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
