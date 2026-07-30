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

    //Anima a entrada da tela
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = this.AnimateEntranceAsync();
    }

    //Abre a tela de detalhe da proposta tocada, com efeito de aperto
    async void OnProposalTapped(object sender, TappedEventArgs e)
    {
        if (sender is not View card || card.BindingContext is not PropostaPsicopedagogicaModel proposta)
            return;

        await card.AnimatePressAsync();
        await Shell.Current.GoToAsync(
            $"{nameof(DetalhePropostaPsicopedagogicaPage)}?propostaId={Uri.EscapeDataString(proposta.Id)}");
    }
}
