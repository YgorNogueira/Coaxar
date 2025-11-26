using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoaxarApp
{
    public class AnimalModel
    {
        //Nome do animal

        private string? _endangeredNational;
        private string? _endangeredInternational;
        private string? _endangeredState;

        private string? _size;

        public required string CommonName { get; set; }

        public string Family { get; set; }

        //Nome científico do animal
        public string? Species { get; set; }

        //Descrição morfológica do animal
        public string? MorphDescription { get; set; }

        public string Feeding { get; set; }

        //Distribuição geográfica do animal
        public string? MicroHabitat { get; set; }

        //Nome do autor que descreveu o animal e ano
        public string? DiscoveryDate { get; set; }

        //Hábito de vida do animal
        //EX: Arborícola
        public string Habit { get; set; }

        //Se está ameaçado
        public string EndangeredNational
        {
            get => _endangeredNational ?? string.Empty;
            set => _endangeredNational = ParseStatus(value);
        }

        public string EndangeredInternational
        {
            get => _endangeredInternational ?? string.Empty;
            set => _endangeredInternational = ParseStatus(value);
        }

        public string EndangeredState
        {
            get => _endangeredState ?? string.Empty;
            set => _endangeredState = ParseStatus(value);
        }

        //Tamanho do macho
        public string Size
        {
            get => _size ?? string.Empty;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _size = string.Empty;
                    return;
                }

                // Normaliza espaços
                var cleaned = string.Join(" ",
                    value.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries));

                // Extrai números (2,9 | 3.8 | 4) do texto
                var matches = Regex.Matches(cleaned, @"\d+(?:[.,]\d+)?");

                if (matches.Count == 0)
                {
                    _size = cleaned; // nada para extrair, mantém texto limpo
                    return;
                }

                // Converte para double (aceitando vírgula ou ponto)
                var nums = matches
                    .Select(m => m.Value.Replace(',', '.'))
                    .Select(s => double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : (double?)null)
                    .Where(d => d.HasValue)
                    .Select(d => d!.Value)
                    .ToList();

                if (nums.Count == 0)
                {
                    _size = cleaned;
                    return;
                }

                var min = nums.Min();
                var max = nums.Max();

                // Formata com vírgula (pt-BR) e sufixo "cm"
                string F(double v) => v.ToString("0.##", new CultureInfo("pt-BR"));

                _size = min == max ? $"{F(min)} cm" : $"{F(min)} - {F(max)} cm";
            }
        }

        //Caminho do arquivo de áudio
        public string? VocalizationPath { get; set; }
        public MediaSource VocalizationSource =>
        MediaSource.FromResource(VocalizationPath);

        private static string ParseStatus(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            // Divide no primeiro " - " e pega a parte após o hífen
            var parts = value.Split('-', 2, StringSplitOptions.TrimEntries);
            var readable = parts.Length == 2 ? parts[1] : parts[0];

            // Normaliza capitalização: primeira letra maiúscula, resto minúsculo
            return char.ToUpper(readable[0]) + readable.Substring(1).ToLowerInvariant();
        }

    }
}
