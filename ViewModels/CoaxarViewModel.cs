using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoaxarApp.ViewModels
{
    public class CoaxarViewModel
    {
        // Lista completa de espécies
        public ObservableCollection<AnimalModel> Species { get; set; } = new ObservableCollection<AnimalModel>();

        // Lista filtrada de espécies
        public ObservableCollection<AnimalModel> FilteredSpecies { get; set; } = new ObservableCollection<AnimalModel>();

        // Construtor da classe
        public CoaxarViewModel()
        {
            Species = new ObservableCollection<AnimalModel>();
            FilteredSpecies = new ObservableCollection<AnimalModel>();
        }
    }
}
