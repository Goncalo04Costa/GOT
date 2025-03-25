using static MudBlazor.CategoryTypes;

namespace GOTinforcavado.Auxiliar
{
    public class TicketForm
    {
        public string EscolhaInicial { get; set; }
        public string Departamento { get; set; }


        public TicketForm(string escolhainicial, string departamento)
        {
            EscolhaInicial = escolhainicial;
            Departamento = departamento;
        }
    }
}
