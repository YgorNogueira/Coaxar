using CoaxarApp.ViewModels;

namespace CoaxarApp
{
    public partial class ListPage : ContentPage
    {
        private CoaxarViewModel ViewModel => BindingContext as CoaxarViewModel;

        public ListPage()
        {
            InitializeComponent();
            BindingContext = new CoaxarViewModel();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue?.ToLower() ?? string.Empty;

            // Filtrar a lista de espécies com base no nome comum ou científico
            var filtered = ViewModel.Species
                .Where(a => a.Name.ToLower().Contains(searchText) ||
                            (a.ScientificName?.ToLower().Contains(searchText) ?? false))
                .ToList();

            // Atualizar a lista filtrada
            ViewModel.FilteredSpecies.Clear();
            foreach (var animal in filtered)
            {
                ViewModel.FilteredSpecies.Add(animal);
            }
        }
    }
}
