using System.Collections.ObjectModel;
using CoaxarApp.Models;

namespace CoaxarApp.ViewModels
{
    public class CoaxarViewModel
    {
        //Coleção de animais exibida na tela
        public ObservableCollection<AnimalModel> Animals { get; set; } = new ObservableCollection<AnimalModel>();
    }
}
