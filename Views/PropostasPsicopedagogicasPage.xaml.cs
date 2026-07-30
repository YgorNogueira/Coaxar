using CoaxarApp.Models;
using CoaxarApp.Services;

namespace CoaxarApp.Views;

public partial class PropostasPsicopedagogicasPage : ContentPage
{
    public PropostasPsicopedagogicasPage()
    {
        InitializeComponent();
        PropostasCollection.ItemsSource = PropostaPsicopedagogicaRepository.GetAll();
    }

    async void OnProposalSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not PropostaPsicopedagogicaModel proposta)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync(
            $"{nameof(DetalhePropostaPsicopedagogicaPage)}?propostaId={Uri.EscapeDataString(proposta.Id)}");
    }
}
