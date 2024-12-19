using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

class GameContent : ContentBase
{
    private string[][] data;

    public GameContent(State state, Window window) : base(state, window)
    {
        // 10×20の2次元配列を初期化
        data = new string[20][];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new string[10];
            for (int j = 0; j < data[i].Length; j++)
            {
                data[i][j] = GetRandomJapaneseChar().ToString(); // サンプルデータ
            }
        }
    }

    public override void Update()
    {
        UpdateContent();
    }

    public override void UpdateContent()
    {
        // Canvasの作成
        var canvas = new Canvas
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Background = Brushes.LightGray,
        };

        // dataの内容に基づいて要素を追加
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                // Borderを作成して枠線を設定
                var border = new Border
                {
                    BorderBrush = Brushes.Black, // 枠線の色
                    BorderThickness = new Thickness(1), // 枠線の太さ
                    Width = 50,  // 横幅
                    Height = 30, // 高さ
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                // Border内にTextBlockを配置
                var textBlock = new TextBlock
                {
                    Text = data[i][j],
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.Black,
                };

                border.Child = textBlock; // TextBlockをBorder内に設定

                // BorderをCanvasに追加し、位置を設定
                canvas.Children.Add(border);
                Canvas.SetLeft(border, j * 50); // 横方向の位置
                Canvas.SetTop(border, i * 30);  // 縦方向の位置
            }
        }

        // Canvasをウィンドウのコンテンツに設定
        window.Content = canvas;
    }

    private static char GetRandomJapaneseChar()
    {
        var rand = new Random();
        // Unicodeで日本語の範囲を指定（ここではひらがな範囲を使用）
        return (char)rand.Next(0x3040, 0x309F); // Unicode範囲: ひらがな
    }
}
