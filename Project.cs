using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

class Project
{
    public static void Main(string[] args)
    {
        AppBuilder.Configure<Application>()
                  .UsePlatformDetect()
                  .Start(AppMain, args);
    }

    public static void AppMain(Application app, string[] args)
    {
        app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
        app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

        var win = new Window{
            Title = "W-mas v0.0",
            Height = 720,
            Width = 1280,
            Background = Brushes.AliceBlue,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        new GameWindowWithCanvas(win);
        app.Run(win);
    }
}