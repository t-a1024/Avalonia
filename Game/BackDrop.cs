using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Layout;

public class BackDrop : Canvas
{
    public BackDrop(){
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
        Background = Brushes.AliceBlue;
    }
}