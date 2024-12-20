using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

public class GameTable
{
    private readonly string[][] data;
    private Border draggingElement;
    private Avalonia.Point originalPosition;
    private (int row, int col)? originalIndex;
    private Canvas canvas;

    public GameTable()
    {
        data = new string[20][];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new string[10];
            for (int j = 0; j < data[i].Length; j++)
            {
                data[i][j] = GetRandomJapaneseChar().ToString();
            }
        }
    }

    public Canvas TableCanvas()
    {
        UpdateTable();
        return canvas;
    }

    private Border CreateBorder(int row, int col)
    {
        var border = new Border
        {
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(1),
            Width = 20,
            Height = 20
        };

        var textBlock = new TextBlock
        {
            Text = data[row][col],
            FontSize = 12,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        border.Child = textBlock;

        border.PointerPressed += (s, e) =>
        {
            draggingElement = border;
            originalPosition = e.GetPosition((Visual)border.Parent!);
            originalIndex = (row, col);
        };

        border.PointerMoved += (s, e) =>
        {
            if (draggingElement != null && e.GetCurrentPoint((Visual)border.Parent!).Properties.IsLeftButtonPressed)
            {
                var position = e.GetPosition((Visual)draggingElement.Parent!);
                Canvas.SetLeft(draggingElement, position.X - 10); // 中心を合わせる
                Canvas.SetTop(draggingElement, position.Y - 10);
            }
        };

        border.PointerReleased += (s, e) =>
        {
            if (draggingElement != null)
            {
                // 新しい位置の計算とデータ更新
                var canvas = (Canvas)draggingElement.Parent!;
                var position = e.GetPosition(canvas);
                var newRow = (int)(position.Y / 20);
                var newCol = (int)(position.X / 20);

                if (newRow >= 0 && newRow < data.Length && newCol >= 0 && newCol < data[0].Length)
                {
                    // 入れ替え
                    var oldIndex = originalIndex!.Value;
                    var pre = data[newRow][newCol];
                    data[newRow][newCol] = data[oldIndex.row][oldIndex.col];
                    data[oldIndex.row][oldIndex.col] = pre;
                    Console.WriteLine("全データ:");
                    for (int i = 0; i < data.Length; i++)
                    {
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            Console.Write(data[i][j] + " ");
                        }
                        Console.WriteLine();
                    }

                    // ドラッグ要素の位置を更新
                    Canvas.SetLeft(draggingElement, newCol * 20);
                    Canvas.SetTop(draggingElement, newRow * 20);

                    // 更新されたデータに基づいてCanvasを再描画
                    UpdateTable();
                }
            }

            // ドラッグ状態を解除
            draggingElement = null;
        };

        return border;
    }

    private void UpdateTable()
    {
        // 新しいCanvasを作成する前に、既存のCanvasの内容をクリア
        if (canvas != null)
        {
            canvas.Children.Clear(); // 古い子要素を削除
        }else{
            canvas = new Canvas
            {
                Background = Brushes.LightGray,
                Width = 200,
                Height = 400
            };
        }

        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                var border = CreateBorder(i, j);
                canvas.Children.Add(border);
                Canvas.SetLeft(border, j * 20);
                Canvas.SetTop(border, i * 20);
            }
        }
    }


    private static char GetRandomJapaneseChar()
{
    // ひらがな50音と濁音をリストに格納
    string[] hiragana =
    [
        "あ", "い", "う", "え", "お", "か", "き", "く", "け", "こ", "さ", "し", "す", "せ", "そ",
        "た", "ち", "つ", "て", "と", "な", "に", "ぬ", "ね", "の", "は", "ひ", "ふ", "へ", "ほ",
        "ま", "み", "む", "め", "も", "や", "ゆ", "よ", "ら", "り", "る", "れ", "ろ", "わ", "を", "ん",
        "が", "ぎ", "ぐ", "げ", "ご", "ざ", "じ", "ず", "ぜ", "ぞ", "だ", "ぢ", "づ", "で", "ど",
        "ば", "び", "ぶ", "べ", "ぼ", "ぱ", "ぴ", "ぷ", "ぺ", "ぽ", "ゃ", "ゅ", "ょ", "っ"
    ];

    var rand = new Random();
    int index = rand.Next(hiragana.Length);
    return hiragana[index][0]; // ランダムにひらがなを選んで返す
}

}
