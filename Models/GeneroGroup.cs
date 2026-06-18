namespace CoaxarApp.Models;

public class GeneroGroup : List<AnimalModel>
{
    public string Nome { get; }
    public string NumeroEspeciesFormatado =>
        Count == 1 ? $"({Count} sp.)" : $"({Count} spp.)";

    public GeneroGroup(string nome, IEnumerable<AnimalModel> animais) : base(animais)
    {
        Nome = nome;
    }
}
