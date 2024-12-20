using System;
using System.Collections.Generic;
using System.Linq;
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

    private static readonly string[] targetWords = ["あいうえお", "あえうえ", "おいお"]; // 消したい単語のリスト
    private readonly List<string> removedWords = [];//　消した単語のリスト


    // 見た目のパラメータ
    public double CellWidth { get; set; } = 20;
    public double CellHeight { get; set; } = 20;
    public double BorderThickness { get; set; } = 1;
    public IBrush BorderBrush { get; set; } = Brushes.Black;
    public IBrush BackgroundBrush { get; set; } = Brushes.LightGray;
    public double FontSize { get; set; } = 12;

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
            BorderBrush = BorderBrush,
            BorderThickness = new Thickness(BorderThickness),
            Width = CellWidth,
            Height = CellHeight
        };

        var textBlock = new TextBlock
        {
            Text = data[row][col],
            FontSize = FontSize,
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
                Canvas.SetLeft(draggingElement, position.X - CellWidth / 2); // 中心を合わせる
                Canvas.SetTop(draggingElement, position.Y - CellHeight / 2);
            }
        };

        border.PointerReleased += (s, e) =>
        {
            if (draggingElement != null)
            {
                var canvas = (Canvas)draggingElement.Parent!;
                var position = e.GetPosition(canvas);
                var newRow = (int)(position.Y / CellHeight);
                var newCol = (int)(position.X / CellWidth);

                if (newRow >= 0 && newRow < data.Length && newCol >= 0 && newCol < data[0].Length)
                {
                    var oldIndex = originalIndex!.Value;
                    var pre = data[newRow][newCol];
                    data[newRow][newCol] = data[oldIndex.row][oldIndex.col];
                    data[oldIndex.row][oldIndex.col] = pre;

                    // 新しい位置に移動後、特定の単語が並んでいるかチェック
                    CheckForMatchingWords();

                    // ドラッグ要素の位置を更新
                    Canvas.SetLeft(draggingElement, newCol * CellWidth);
                    Canvas.SetTop(draggingElement, newRow * CellHeight);

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
                Background = BackgroundBrush,
                Width = CellWidth * data[0].Length,
                Height = CellHeight * data.Length
            };
        }

        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                var border = CreateBorder(i, j);
                canvas.Children.Add(border);
                Canvas.SetLeft(border, j * CellWidth);
                Canvas.SetTop(border, i * CellHeight);
            }
        }
    }

    // 特定の単語が並んでいるかを確認して消去
    private void CheckForMatchingWords()
    {
        int maxWordLength = targetWords.Max(word => word.Length);

        // 横方向のチェック（左から右、右から左）
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                for (int length = 2; length <= maxWordLength; length++) // 長さ2以上の単語をチェック
                {
                    if (j + length <= data[i].Length)
                    {
                        // 左から右のチェック
                        var wordLR = string.Join("", data[i].Skip(j).Take(length));
                        if (Array.Exists(targetWords, element => element == wordLR))
                        {
                            removedWords.Add(wordLR); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i][j + k] = ""; // 一致した単語を消去
                        }
                    }

                    if (j - length + 1 >= 0)
                    {
                        // 右から左のチェック
                        var wordRL = string.Join("", data[i].Skip(j - length + 1).Take(length).Reverse());
                        if (Array.Exists(targetWords, element => element == wordRL))
                        {
                            removedWords.Add(wordRL); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i][j - k] = ""; // 一致した単語を消去
                        }
                    }
                }
            }
        }

        // 縦方向のチェック（上から下、下から上）
        for (int j = 0; j < data[0].Length; j++)
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int length = 2; length <= maxWordLength; length++) // 長さ2以上の単語をチェック
                {
                    if (i + length <= data.Length)
                    {
                        // 上から下のチェック
                        var wordUD = string.Join("", Enumerable.Range(0, length).Select(k => data[i + k][j]));
                        if (Array.Exists(targetWords, element => element == wordUD))
                        {
                            removedWords.Add(wordUD); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i + k][j] = ""; // 一致した単語を消去
                        }
                    }

                    if (i - length + 1 >= 0)
                    {
                        // 下から上のチェック
                        var wordDU = string.Join("", Enumerable.Range(0, length).Select(k => data[i - k][j]));
                        if (Array.Exists(targetWords, element => element == wordDU))
                        {
                            removedWords.Add(wordDU); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i - k][j] = ""; // 一致した単語を消去
                        }
                    }
                }
            }
        }

        // 斜め方向のチェック
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                for (int length = 2; length <= maxWordLength; length++) // 長さ2以上の単語をチェック
                {
                    if (i + length <= data.Length && j + length <= data[i].Length)
                    {
                        // 左上から右下
                        var wordLU = string.Join("", Enumerable.Range(0, length).Select(k => data[i + k][j + k]));
                        if (Array.Exists(targetWords, element => element == wordLU))
                        {
                            removedWords.Add(wordLU); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i + k][j + k] = ""; // 一致した単語を消去
                        }
                    }

                    if (i + length <= data.Length && j - length + 1 >= 0)
                    {
                        // 右上から左下
                        var wordRU = string.Join("", Enumerable.Range(0, length).Select(k => data[i + k][j - k]));
                        if (Array.Exists(targetWords, element => element == wordRU))
                        {
                            removedWords.Add(wordRU); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i + k][j - k] = ""; // 一致した単語を消去
                        }
                    }

                    if (i - length + 1 >= 0 && j + length <= data[i].Length)
                    {
                        // 左下から右上
                        var wordLD = string.Join("", Enumerable.Range(0, length).Select(k => data[i - k][j + k]));
                        if (Array.Exists(targetWords, element => element == wordLD))
                        {
                            removedWords.Add(wordLD); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i - k][j + k] = ""; // 一致した単語を消去
                        }
                    }

                    if (i - length + 1 >= 0 && j - length + 1 >= 0)
                    {
                        // 右下から左上
                        var wordRD = string.Join("", Enumerable.Range(0, length).Select(k => data[i - k][j - k]));
                        if (Array.Exists(targetWords, element => element == wordRD))
                        {
                            removedWords.Add(wordRD); // 消去した単語を記録
                            for (int k = 0; k < length; k++) data[i - k][j - k] = ""; // 一致した単語を消去
                        }
                    }
                }
            }
        }
    }


    private static char GetRandomJapaneseChar()
    {
        string[] hiragana =
        [
            "あ", "い", "う", "え", "お"
            // "あ", "い", "う", "え", "お", "か", "き", "く", "け", "こ", "さ", "し", "す", "せ", "そ",
            // "た", "ち", "つ", "て", "と", "な", "に", "ぬ", "ね", "の", "は", "ひ", "ふ", "へ", "ほ",
            // "ま", "み", "む", "め", "も", "や", "ゆ", "よ", "ら", "り", "る", "れ", "ろ", "わ", "を", "ん",
            // "が", "ぎ", "ぐ", "げ", "ご", "ざ", "じ", "ず", "ぜ", "ぞ", "だ", "ぢ", "づ", "で", "ど",
            // "ば", "び", "ぶ", "べ", "ぼ", "ぱ", "ぴ", "ぷ", "ぺ", "ぽ", "ゃ", "ゅ", "ょ", "っ"
        ];

        var rand = new Random();
        int index = rand.Next(hiragana.Length);
        return hiragana[index][0];
    }
}
