using SQLite;

namespace CoaxarApp.Models
{
    [Table("Animais")]
    public class AnimalModel
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; } = string.Empty;

        public string? ScientificName { get; set; }

        public string? MorphDescription { get; set; }

        public string? Distribution { get; set; }

        // Autor e ano de descrição, ex: " (Cope, 1862)"
        public string? DiscoveryDate { get; set; }

        public string? Habitat { get; set; }

        [Indexed]
        public int FamiliaId { get; set; }

        public string? Familia { get; set; }

        [Indexed]
        public string? Genero { get; set; }

        // Caminho da imagem principal da espécie
        public string? ImagemPath { get; set; }

        // Imagem em maior resolução usada no cabeçalho da tela de detalhe.
        public string? ImagemDetalhePath { get; set; }

        // Caminho da imagem do espectrograma
        public string? EspectrogramaPath { get; set; }

        // Caminho do mapa de distribuição
        public string? MapaPath { get; set; }

        // Crédito da foto para exibição na tela de detalhe
        public string? FotoCredito { get; set; }

        public string? CodigoSonoteca { get; set; }

        public string Habit { get; set; } = string.Empty;

        public string WayOfLife { get; set; } = string.Empty;

        public bool Endangered { get; set; }

        public float MaleSize { get; set; }

        [Ignore]
        public string MaleSizeAsString =>
            MaleSize != 0 ? $"~{MaleSize:G} cm" : "Não informado";

        public float FemaleSize { get; set; }

        [Ignore]
        public string FemaleSizeAsString =>
            FemaleSize != 0 ? $"~{FemaleSize:G} cm" : "Não informado";

        // Canto de anúncio
        public string VocalizationPath { get; set; } = string.Empty;

        // Canto de soltura
        public string? CantoDeSolturaPath { get; set; }
    }
}
