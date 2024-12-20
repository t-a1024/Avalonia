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

    private static readonly string[] targetWords = { "あい", "うえ", "おお" }; // 消したい単語のリスト

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
                var canvas = (Canvas)draggingElement.Parent!;
                var position = e.GetPosition(canvas);
                var newRow = (int)(position.Y / 20);
                var newCol = (int)(position.X / 20);

                if (newRow >= 0 && newRow < data.Length && newCol >= 0 && newCol < data[0].Length)
                {
                    var oldIndex = originalIndex!.Value;
                    var pre = data[newRow][newCol];
                    data[newRow][newCol] = data[oldIndex.row][oldIndex.col];
                    data[oldIndex.row][oldIndex.col] = pre;

                    // 新しい位置に移動後、特定の単語が並んでいるかチェック
                    CheckForMatchingWords();

                    // ドラッグ要素の位置を更新
                    Canvas.SetLeft(draggingElement, newCol * 20);
                    Canvas.SetTop(draggingElement, newRow * 20);

                    UpdateTable();
                }
            }

            draggingElement = null;
        };

        return border;
    }

    private void UpdateTable()
    {
        if (canvas != null)
        {
            canvas.Children.Clear();
        }
        else
        {
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

    // 特定の単語が並んでいるかを確認して消去
    private void CheckForMatchingWords()
    {
        // 横方向のチェック（左から右、右から左）
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j <= data[i].Length - 2; j++)
            {
                // 左から右のチェック
                var wordLR = data[i][j] + data[i][j + 1];
                if (Array.Exists(targetWords, element => element == wordLR))
                {
                    data[i][j] = "";
                    data[i][j + 1] = "";
                }

                // 右から左のチェック
                var wordRL = data[i][j + 1] + data[i][j];
                if (Array.Exists(targetWords, element => element == wordRL))
                {
                    data[i][j] = "";
                    data[i][j + 1] = "";
                }
            }
        }

        // 縦方向のチェック（上から下、下から上）
        for (int j = 0; j < data[0].Length; j++)
        {
            for (int i = 0; i <= data.Length - 2; i++)
            {
                // 上から下のチェック
                var wordUD = data[i][j] + data[i + 1][j];
                if (Array.Exists(targetWords, element => element == wordUD))
                {
                    data[i][j] = "";
                    data[i + 1][j] = "";
                }

                // 下から上のチェック
                var wordDU = data[i + 1][j] + data[i][j];
                if (Array.Exists(targetWords, element => element == wordDU))
                {
                    data[i][j] = "";
                    data[i + 1][j] = "";
                }
            }
        }

        // 斜め方向のチェック（左上から右下、右上から左下、左下から右上、右下から左上）
        for (int i = 0; i < data.Length - 1; i++)
        {
            for (int j = 0; j < data[i].Length - 1; j++)
            {
                // 左上から右下のチェック
                var wordLR = data[i][j] + data[i + 1][j + 1];
                if (Array.Exists(targetWords, element => element == wordLR))
                {
                    data[i][j] = "";
                    data[i + 1][j + 1] = "";
                }

                // 右上から左下のチェック
                var wordRL = data[i][j + 1] + data[i + 1][j];
                if (Array.Exists(targetWords, element => element == wordRL))
                {
                    data[i][j + 1] = "";
                    data[i + 1][j] = "";
                }

                // 左下から右上のチェック
                var wordLD = data[i + 1][j] + data[i][j + 1];
                if (Array.Exists(targetWords, element => element == wordLD))
                {
                    data[i + 1][j] = "";
                    data[i][j + 1] = "";
                }

                // 右下から左上のチェック
                var wordRD = data[i + 1][j + 1] + data[i][j];
                if (Array.Exists(targetWords, element => element == wordRD))
                {
                    data[i + 1][j + 1] = "";
                    data[i][j] = "";
                }
            }
        }
    }


    private static char GetRandomJapaneseChar()
    {
        string[] hiragana =
        {
            "あ", "い", "う", "え", "お", "か", "き", "く", "け", "こ", "さ", "し", "す", "せ", "そ",
            "た", "ち", "つ", "て", "と", "な", "に", "ぬ", "ね", "の", "は", "ひ", "ふ", "へ", "ほ",
            "ま", "み", "む", "め", "も", "や", "ゆ", "よ", "ら", "り", "る", "れ", "ろ", "わ", "を", "ん",
            "が", "ぎ", "ぐ", "げ", "ご", "ざ", "じ", "ず", "ぜ", "ぞ", "だ", "ぢ", "づ", "で", "ど",
            "ば", "び", "ぶ", "べ", "ぼ", "ぱ", "ぴ", "ぷ", "ぺ", "ぽ", "ゃ", "ゅ", "ょ", "っ"
        };

        var rand = new Random();
        int index = rand.Next(hiragana.Length);
        return hiragana[index][0];
    }
}
