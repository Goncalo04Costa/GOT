/*
*	<copyright file="ClientesContactosTel" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 08/04/2025 15:03:04</date>
*	<description></description>
**/

namespace Shared.models
{
    public class ClientesContactosTel
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }

        public DateTime DataCriacao { get; set; }
        public virtual ClientesContactos ClientesContactos { get; set; }
        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public ClientesContactosTel()
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
