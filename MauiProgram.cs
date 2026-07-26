using Microsoft.Extensions.Logging;

namespace CoaxarApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("RussoOne-Regular.ttf", "RussoOne");
			});

#if ANDROID
		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(
			"RemoveUnderline",
			(handler, _) =>
			{
				handler.PlatformView.BackgroundTintList =
					Android.Content.Res.ColorStateList.ValueOf(
						Android.Graphics.Color.Transparent);
			});
#endif

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
