using CoaxarApp.Services;

namespace CoaxarApp.Views;

[QueryProperty(nameof(PropostaId), "propostaId")]
public partial class DetalhePropostaPsicopedagogicaPage : ContentPage
{
    public string PropostaId
    {
        set
        {
            var proposta = PropostaPsicopedagogicaRepository.GetById(
                Uri.UnescapeDataString(value ?? string.Empty));

            if (proposta is not null)
                BindingContext = proposta;
        }
    }

    public DetalhePropostaPsicopedagogicaPage()
    {
        InitializeComponent();
    }

    //Anima a entrada da tela
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = this.AnimateEntranceAsync();
    }
}
