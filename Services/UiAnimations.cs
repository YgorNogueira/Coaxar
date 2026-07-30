namespace CoaxarApp.Services;

//Animações reutilizáveis da interface
public static class UiAnimations
{
    //Efeito de "aperto": o botão encolhe e volta ao tamanho normal
    public static async Task AnimatePressAsync(this View view)
    {
        await view.ScaleTo(0.94, 70, Easing.CubicOut);
        await view.ScaleTo(1.0, 90, Easing.CubicOut);
    }

    //Entrada suave da tela: o conteúdo surge com fade e um leve deslize para cima
    public static async Task AnimateEntranceAsync(this ContentPage page)
    {
        if (page.Content is null)
            return;

        var content = page.Content;
        content.Opacity = 0;
        content.TranslationY = 16;

        await Task.WhenAll(
            content.FadeTo(1, 220, Easing.CubicOut),
            content.TranslateTo(0, 0, 220, Easing.CubicOut));
    }
}
