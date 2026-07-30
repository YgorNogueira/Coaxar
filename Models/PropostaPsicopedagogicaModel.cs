namespace CoaxarApp.Models;

public class PropostaPsicopedagogicaModel
{
    public string Id { get; init; } = string.Empty;

    public string Nivel { get; init; } = string.Empty;

    public string Duracao { get; init; } = string.Empty;

    public string Area { get; init; } = string.Empty;

    public string Resumo { get; init; } = string.Empty;

    public IReadOnlyList<string> HabilidadesBncc { get; init; } = [];

    public IReadOnlyList<string> Objetivos { get; init; } = [];

    public IReadOnlyList<string> Recursos { get; init; } = [];

    public IReadOnlyList<PropostaEtapaModel> Etapas { get; init; } = [];

    public IReadOnlyList<string> Avaliacao { get; init; } = [];
}

public class PropostaEtapaModel
{
    public string Duracao { get; init; } = string.Empty;

    public string Titulo { get; init; } = string.Empty;

    public string Descricao { get; init; } = string.Empty;
}
