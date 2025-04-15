/*
*	<copyright file="ClientesContactos" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 08/04/2025 15:01:22</date>
*	<description></description>
**/

namespace Shared.models
{
    public class ClientesContactos
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Contacto { get; set; }

        public bool Ativo {  get; set; }
        public DateTime DataCriacao { get; set; }
        public string Email { get; set; }

        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public ClientesContactos()
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
