using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

public partial class FamiliasPage : ContentPage
{
    private int _searchVersion;

    public FamiliasPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await AtualizarFamiliasAsync(BuscaEntry.Text);
    }

    async void OnBuscaTextChanged(object sender, TextChangedEventArgs e) =>
        await AtualizarFamiliasAsync(e.NewTextValue);

    private async Task AtualizarFamiliasAsync(string? busca)
    {
        var version = ++_searchVersion;
        var familias = await AnimalRepository.GetFamiliasAsync(busca);

        if (version == _searchVersion)
            FamiliasCollection.ItemsSource = familias;
    }

    async void OnFamiliaSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not FamiliaModel familia)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync(
            $"{nameof(EspeciesDaFamiliaPage)}?familiaNome={Uri.EscapeDataString(familia.Nome)}");
    }
}
