using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

[QueryProperty(nameof(FamiliaNome), "familiaNome")]
public partial class EspeciesDaFamiliaPage : ContentPage
{
    private string _familiaNome = string.Empty;
    private int _searchVersion;

    public string FamiliaNome
    {
        set
        {
            _familiaNome = Uri.UnescapeDataString(value ?? string.Empty);
            FamiliaTitulo.Text = _familiaNome;
            _ = AtualizarEspeciesAsync();
        }
    }

    public EspeciesDaFamiliaPage()
    {
        InitializeComponent();
    }

    async void OnBuscaTextChanged(object sender, TextChangedEventArgs e) =>
        await AtualizarEspeciesAsync(e.NewTextValue);

    private async Task AtualizarEspeciesAsync(string? busca = null)
    {
        if (string.IsNullOrWhiteSpace(_familiaNome))
            return;

        var version = ++_searchVersion;
        var grupos = await AnimalRepository.GetGenerosDaFamiliaAsync(
            _familiaNome,
            busca);

        if (version == _searchVersion)
            EspeciesCollection.ItemsSource = grupos;
    }

    async void OnEspecieSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not AnimalModel animal)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync($"{nameof(DetalheEspeciePage)}?animalId={animal.Id}");
    }
}
