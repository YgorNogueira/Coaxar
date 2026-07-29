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

    //Carrega as famílias ao abrir a tela
    protected override async void OnAppearing()
    {
        base.OnAppearing();
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

    //Abre a tela de espécies da família selecionada
    async void OnFamilySelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not FamiliaModel familia)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync(
            $"{nameof(EspeciesDaFamiliaPage)}?familiaNome={Uri.EscapeDataString(familia.Nome)}");
    }
}
