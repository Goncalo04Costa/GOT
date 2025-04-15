/*
*	<copyright file="UtilizadorEquipa" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 08/04/2025 14:39:35</date>
*	<description></description>
**/

namespace Shared.models
{
    public class UtilizadorEquipa
    {
        #region ATRIBUTOS
        public int UtilizadorId { get; set; }
        public virtual Utilizador Utilizador { get; set; }

        public int EquipaId { get; set; }
        public virtual Equipas Equipas { get; set; }
        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public UtilizadorEquipa()
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
