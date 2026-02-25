using Microsoft.Extensions.Logging;
#if ANDROID
using Microsoft.Maui.Handlers;
#endif
#if IOS || MACCATALYST
using Microsoft.Maui.Handlers;
using UIKit;
#endif

namespace maui_bmi;

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

#if ANDROID
        EntryHandler.Mapper.AppendToMapping("CleanInputChrome", (handler, view) =>
        {
            handler.PlatformView.Background = null;
            handler.PlatformView.SetPadding(0, 0, 0, 0);
        });

        PickerHandler.Mapper.AppendToMapping("CleanInputChrome", (handler, view) =>
        {
            handler.PlatformView.Background = null;
            handler.PlatformView.SetPadding(0, 0, 0, 0);
        });
#endif

#if IOS || MACCATALYST
        EntryHandler.Mapper.AppendToMapping("CleanInputChrome", (handler, view) =>
        {
            handler.PlatformView.BorderStyle = UITextBorderStyle.None;
            handler.PlatformView.BackgroundColor = UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.Layer.ShadowOpacity = 0;
        });

        PickerHandler.Mapper.AppendToMapping("CleanInputChrome", (handler, view) =>
        {
            handler.PlatformView.BorderStyle = UITextBorderStyle.None;
            handler.PlatformView.BackgroundColor = UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.Layer.ShadowOpacity = 0;
        });
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}