using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls.Shapes;
using System;

public class BackDrop : Canvas
{
    public BackDrop()
    {

        Update();
    }

    public void Update(){
        // 長方形で背景を塗りつぶす
        var rectangle = new Rectangle
        {
            Fill = Brushes.LightBlue,  // 塗りつぶし色
            Width = 9999,
            Height = 9999
        };

        // CanvasにRectangleを追加
        Children.Add(rectangle);

        // サイズ変更イベントの登録
        this.SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        // 画面サイズに合わせてRectangleを調整
        var rectangle = Children[0] as Rectangle;
        rectangle.Width = e.NewSize.Width;
        rectangle.Height = e.NewSize.Height;
    }
}
