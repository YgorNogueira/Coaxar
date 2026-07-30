using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

public partial class FamiliasPage : ContentPage
{
    //Versão da busca para ignorar resultados atrasados
    private int _searchVersion;

    public FamiliasPage()
    {
        InitializeComponent();
    }

    //Carrega as famílias ao abrir a tela, com animação de entrada
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _ = this.AnimateEntranceAsync();
        await UpdateFamiliesAsync(BuscaEntry.Text);
    }

    //Atualiza a lista quando o texto da busca muda
    async void OnSearchTextChanged(object sender, TextChangedEventArgs e) =>
        await UpdateFamiliesAsync(e.NewTextValue);

    //Busca as famílias no banco e atualiza a tela
    private async Task UpdateFamiliesAsync(string? search)
    {
        var version = ++_searchVersion;
        var familias = await AnimalRepository.GetFamiliesAsync(search);

        if (version == _searchVersion)
            FamiliasCollection.ItemsSource = familias;
    }

    //Abre a tela de espécies da família tocada, com efeito de aperto
    async void OnFamilyTapped(object sender, TappedEventArgs e)
    {
        if (sender is not View card || card.BindingContext is not FamiliaModel familia)
            return;

        await card.AnimatePressAsync();
        await Shell.Current.GoToAsync(
            $"{nameof(EspeciesDaFamiliaPage)}?familiaNome={Uri.EscapeDataString(familia.Nome)}");
    }
}
