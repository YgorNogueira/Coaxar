using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

[QueryProperty(nameof(FamiliaNome), "familiaNome")]
public partial class EspeciesDaFamiliaPage : ContentPage
{
    private string _familiaNome = string.Empty;

    //Versão da busca para ignorar resultados atrasados
    private int _searchVersion;

    //Nome da família recebido pela navegação
    public string FamiliaNome
    {
        set
        {
            _familiaNome = Uri.UnescapeDataString(value ?? string.Empty);
            FamiliaTitulo.Text = _familiaNome;
            _ = UpdateSpeciesAsync();
        }
    }

    public EspeciesDaFamiliaPage()
    {
        InitializeComponent();
    }

    //Anima a entrada da tela
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = this.AnimateEntranceAsync();
    }

    //Atualiza a lista quando o texto da busca muda
    async void OnSearchTextChanged(object sender, TextChangedEventArgs e) =>
        await UpdateSpeciesAsync(e.NewTextValue);

    //Busca as espécies agrupadas por gênero e atualiza a tela
    private async Task UpdateSpeciesAsync(string? search = null)
    {
        if (string.IsNullOrWhiteSpace(_familiaNome))
            return;

        var version = ++_searchVersion;
        var grupos = await AnimalRepository.GetGeneraByFamilyAsync(
            _familiaNome,
            search);

        if (version == _searchVersion)
            EspeciesCollection.ItemsSource = grupos;
    }

    //Abre a tela de detalhe da espécie tocada, com efeito de aperto
    async void OnSpeciesTapped(object sender, TappedEventArgs e)
    {
        if (sender is not View card || card.BindingContext is not AnimalModel animal)
            return;

        await card.AnimatePressAsync();
        await Shell.Current.GoToAsync($"{nameof(DetalheEspeciePage)}?animalId={animal.Id}");
    }
}
