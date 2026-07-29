namespace CoaxarApp.Models;

//Grupo de animais de um mesmo gênero para a lista agrupada
public class GeneroGroup : List<AnimalModel>
{
    //Nome do gênero
    public string Nome { get; }

    //Quantidade de espécies formatada
    //EX: (2 spp.)
    public string NumeroEspeciesFormatado =>
        Count == 1 ? $"({Count} sp.)" : $"({Count} spp.)";

    public GeneroGroup(string nome, IEnumerable<AnimalModel> animais) : base(animais)
    {
        Nome = nome;
    }
}
