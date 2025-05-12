/*
*	<copyright file="Empresa" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 5/8/2025 10:22:38 AM</date>
*	<description></description>
**/

namespace Shared.models
{
    public class Empresa
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }

        public List<Utilizador> Utilizadores { get; set; } = new();

        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public Empresa()
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
