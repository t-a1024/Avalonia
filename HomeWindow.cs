using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

internal class HomeWindow
{
    public Window win;

    public HomeWindow(Window window)
    {
        win = window;

        var label = new Label
        {
            Content = "Hello World",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        var StartButton = new Button
        {
            Content = "Start",
            FontSize = 24,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        var SettingButton = new Button
        {
            Content = "Setting",
            FontSize = 24,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        var stack = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 10 // ボタン間のスペースを追加
        };

        stack.Children.Add(label);
        stack.Children.Add(StartButton);
        stack.Children.Add(SettingButton);

        win.Content = stack;
        /* ボタンを押された際、GameWindowまたはSettingWindowに切り替えたい */

        win.Show();
    }
}