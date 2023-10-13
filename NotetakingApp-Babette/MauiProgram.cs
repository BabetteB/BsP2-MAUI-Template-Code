using Microsoft.Extensions.Logging;
using NotetakingApp_Babette.Pages;
using NotetakingApp_Babette.ViewModels;

namespace NotetakingApp_Babette;

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
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MakeNotePage>();
        builder.Services.AddSingleton<NoteViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
