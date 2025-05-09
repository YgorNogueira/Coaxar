using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoaxarApp
{
    public class AnimalModel
    {
        //Nome do animal
        public required string Name { get; set; }

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

        //Hábito de vida do animal
        //EX: Arborícola
        public string Habit { get; set; }

        //Se é diurno ou noturno
        public string WayOfLife { get; set; }

        //Se está ameaçado
        public bool Endangered { get; set; }

        //Tamanho do macho
        public float MaleSize { get; set; }
        public string MaleSizeAsString { 
            get 
            {
                if (MaleSize != 0)
                    return $"{MaleSize} cm";
                else
                    return "Não informado";
            } 
        }

        //Tamanho da fêmea
        public float FemaleSize { get; set; }
        public string FemaleSizeAsString
        {
            get
            {
                if (FemaleSize != 0)
                    return $"{FemaleSize} cm";
                else
                    return "Não informado";
            }
        }

        //Caminho do arquivo de áudio
        public string VocalizationPath { get; set; }

    }
}
