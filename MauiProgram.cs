using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;

namespace CoaxarApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OSR");
				fonts.AddFont("OpenSans-Semibold.ttf", "OSS");
				fonts.AddFont("Poppins-Regular.ttf", "POPR");
				fonts.AddFont("Poppins-SemiBold.ttf", "POPS");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
