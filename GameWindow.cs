using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

internal class GameWindow
{
    public Window win;

    private string[][] data =
    {
        new string[] { "A1", "B1", "C1" },
        new string[] { "A2", "B2", "C2" },
        new string[] { "A3", "B3", "C3" }
    };

    private (int row, int col)? draggedCell = null; // ドラッグ中のセルの位置を記録
    Grid grid; // Gridのインスタンス

    public GameWindow(Window window)
    {
        win = window;

        grid = new Grid
        {
            ShowGridLines = true,
            Background = Brushes.Navy
        };

        UpdateGridStructure();
        UpdateGridContent();

        win.Content = grid;
        win.Show();
    }

    private void UpdateGridStructure()
    {
        grid.RowDefinitions.Clear();
        for (int i = 0; i < data.Length; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        grid.ColumnDefinitions.Clear();
        for (int j = 0; j < data[0].Length; j++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }
    }

    private void UpdateGridContent()
    {
        grid.Children.Clear();

        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                var label = new Label
                {
                    Content = data[i][j],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Background = Brushes.LightGray,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Avalonia.Thickness(1),
                    Padding = new Avalonia.Thickness(5)
                };

                // ドラッグ＆ドロップ用のイベントを設定
                label.PointerPressed += (s, e) => OnDragStart(i, j);
                label.PointerReleased += (s, e) => OnDrop(i, j);

                Grid.SetRow(label, i);
                Grid.SetColumn(label, j);
                grid.Children.Add(label);
            }
        }
    }

    private void OnDragStart(int row, int col)
    {
        Console.WriteLine($"Drag started at ({row}, {col})");
        draggedCell = (row, col); // ドラッグ元のセルを記録
    }

    private void OnDrop(int row, int col)
    {
        if (draggedCell == null)
            return;

        Console.WriteLine($"Dropped at ({row}, {col})");

        // ドラッグ元とドロップ先が異なる場合、内容を入れ替える
        var (srcRow, srcCol) = draggedCell.Value;
        if (srcRow != row || srcCol != col)
        {
            string temp = data[srcRow][srcCol];
            data[srcRow][srcCol] = data[row][col];
            data[row][col] = temp;

            // 表示を更新
            UpdateGridContent();
        }

        // ドラッグ状態をリセット
        draggedCell = null;
    }
}
