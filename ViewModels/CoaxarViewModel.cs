using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CoaxarApp
{
    public class CoaxarViewModel
    {
        public ObservableCollection<AnimalModel> Animals { get; set; } = new ObservableCollection<AnimalModel>();
        public AnimalModel? SelectedAnimal { get; private set; }
        public ICommand OpenDetailsCommand => new Command<AnimalModel>(async (animal) =>
        {
            if (animal == null)
                return;
            SelectedAnimal = animal;
            await Shell.Current.GoToAsync(nameof(DetailsPage));
        });

    }
}
