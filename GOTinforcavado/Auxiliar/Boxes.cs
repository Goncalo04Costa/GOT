using Microsoft.AspNetCore.Components;
using static MudBlazor.CategoryTypes;

namespace GOTinforcavado.Auxiliar
{
    public class Boxes
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public string Imagem { get; set; }
        public string AltImagem { get; set; }

        public Boxes(string titulo, string descricao, string link, string imagem, string altImagem)
        {
            Titulo = titulo;
            Descricao = descricao;
            Link = link;
            Imagem = imagem;
            AltImagem = altImagem;
        }

    }



}
