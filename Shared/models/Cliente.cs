/*
*	<copyright file="Cliente" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 08/04/2025 14:58:46</date>
*	<description></description>
**/

namespace Shared.models
{
    public class Cliente
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Contribuinte { get; set; }
        public int Email { get; set; }
        public string Morada { get; set; }

        public string Telefone { get; set; }
        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public Cliente()
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
