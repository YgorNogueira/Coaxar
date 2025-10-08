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
        public bool EndangeredNational { get; set; }
        public bool EndangeredInternational { get; set; }
        public bool EndangeredState { get; set; }

        //Tamanho do macho
        public string Size { get; set; }

        //Caminho do arquivo de áudio
        public string? VocalizationPath { get; set; }

    }
}
