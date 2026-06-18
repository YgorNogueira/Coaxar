using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

[QueryProperty(nameof(FamiliaNome), "familiaNome")]
public partial class EspeciesDaFamiliaPage : ContentPage
{
    private string _familiaNome = string.Empty;

    public string FamiliaNome
    {
        set
        {
            _familiaNome = Uri.UnescapeDataString(value ?? string.Empty);
            FamiliaTitulo.Text = _familiaNome;
            AtualizarEspecies();
        }
    }

    public EspeciesDaFamiliaPage()
    {
        InitializeComponent();
    }

    void OnBuscaTextChanged(object sender, TextChangedEventArgs e) =>
        AtualizarEspecies(e.NewTextValue);

    private void AtualizarEspecies(string? busca = null)
    {
        if (string.IsNullOrWhiteSpace(_familiaNome))
            return;

        EspeciesCollection.ItemsSource =
            AnimalRepository.GetGenerosDaFamilia(_familiaNome, busca);
    }

    async void OnEspecieSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not AnimalModel animal)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync($"{nameof(DetalheEspeciePage)}?animalId={animal.Id}");
    }
}
