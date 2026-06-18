using System.Collections.ObjectModel;
using CoaxarApp.Models;

namespace CoaxarApp.ViewModels
{
    public class CoaxarViewModel
    {
        public ObservableCollection<AnimalModel> Animals { get; set; } = new ObservableCollection<AnimalModel>();
    }
}
