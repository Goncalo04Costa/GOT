/*
*	<copyright file="ChatBotMessages" company="IPCA">
*	</copyright>
* 	<author>Gonçalo Costa</author>
*	<contact>a26024@alunos.ipca.pt</contact>
*   <date>2025 5/5/2025 12:20:10 PM</date>
*	<description></description>
**/

namespace Shared.models
{
    public class ChatBotMessages
    {
        #region ATRIBUTOS
        public int Id { get; set; } 
        public string PalavrasChave { get; set; } = string.Empty;   
        public string Message { get; set; } = string.Empty; 
        #endregion

        #region COMPORTAMENTO

        #region CONSTRUTORES
        public ChatBotMessages()
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
