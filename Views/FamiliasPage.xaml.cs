using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

public partial class FamiliasPage : ContentPage
{
    public FamiliasPage()
    {
        InitializeComponent();
        AtualizarFamilias();
    }

    void OnBuscaTextChanged(object sender, TextChangedEventArgs e) =>
        AtualizarFamilias(e.NewTextValue);

    private void AtualizarFamilias(string? busca = null) =>
        FamiliasCollection.ItemsSource = AnimalRepository.GetFamilias(busca);

    async void OnFamiliaSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not FamiliaModel familia)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync(
            $"{nameof(EspeciesDaFamiliaPage)}?familiaNome={Uri.EscapeDataString(familia.Nome)}");
    }
}
