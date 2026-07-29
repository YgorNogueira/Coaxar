using SQLite;

namespace CoaxarApp.Models
{
    [Table("Animais")]
    public class AnimalModel
    {
        //Identificador do animal no banco
        [PrimaryKey]
        public int Id { get; set; }

        //Nome do animal
        [NotNull]
        public string Name { get; set; } = string.Empty;

        //Nome científico do animal
        public string? ScientificName { get; set; }

        //Descrição morfológica do animal
        public string? MorphDescription { get; set; }

        //Distribuição geográfica do animal
        public string? Distribution { get; set; }

        //Nome do autor que descreveu o animal e ano
        public string? DiscoveryDate { get; set; }

        //Locais onde a espécie ocorre
        public string? Habitat { get; set; }

        //Identificador da família no banco
        [Indexed]
        public int FamiliaId { get; set; }

        //Nome da família do animal
        public string? Familia { get; set; }

        //Gênero do animal
        [Indexed]
        public string? Genero { get; set; }

        //Caminho da imagem principal da espécie
        public string? ImagemPath { get; set; }

        //Imagem em maior resolução usada no cabeçalho da tela de detalhe
        public string? ImagemDetalhePath { get; set; }

        //Caminho da imagem do espectrograma
        public string? EspectrogramaPath { get; set; }

        //Caminho do mapa de distribuição
        public string? MapaPath { get; set; }

        //Crédito da foto para exibição na tela de detalhe
        public string? FotoCredito { get; set; }

        //Código da espécie na sonoteca
        public string? CodigoSonoteca { get; set; }

        //Hábito de vida do animal
        //EX: Arborícola
        public string Habit { get; set; } = string.Empty;

        //Se é diurno ou noturno
        public string WayOfLife { get; set; } = string.Empty;

        //Se está ameaçado
        public bool Endangered { get; set; }

        //Tamanho do macho
        public float MaleSize { get; set; }

        //Tamanho do macho formatado para exibição
        [Ignore]
        public string MaleSizeAsString =>
            MaleSize != 0 ? $"~{MaleSize:G} cm" : "Não informado";

        //Tamanho da fêmea
        public float FemaleSize { get; set; }

        //Tamanho da fêmea formatado para exibição
        [Ignore]
        public string FemaleSizeAsString =>
            FemaleSize != 0 ? $"~{FemaleSize:G} cm" : "Não informado";

        //Canto de anúncio
        public string VocalizationPath { get; set; } = string.Empty;

        //Canto de soltura
        public string? CantoDeSolturaPath { get; set; }
    }
}
