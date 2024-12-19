using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

internal class GameWindowWithCanvas
{
    public Window win;


    // ランダムな日本語の一文字を生成する
    private static char GetRandomJapaneseChar()
    {
        var rand = new Random();
        // Unicodeで日本語の範囲を指定（ここではひらがな範囲を使用）
        return (char)rand.Next(0x3040, 0x309F); // Unicode範囲: ひらがな
    }
    // 2次元配列のデータ（表として表示するデータ）
    private readonly string[][] data =
    [
        ["A1", "B1", "C1"],
        ["A2", "B2", "C2"],
        ["A3", "B3", "C3"]
    ];
    

    // Canvasを保持するインスタンス
    readonly Canvas canvas;

    // ドラッグ中のセルの位置（行と列を保持）
    private (int row, int col)? draggedCell = null;

    // ドラッグ中のTextBlock（対象のUI要素を保持）
    private TextBlock draggedTextBlock = null;

    // コンストラクタ（ウィンドウを初期化し、Canvasを設定）
    public GameWindowWithCanvas(Window window)
    {
        win = window;

        for (int i = 0; i < 3; i++)
        {
            data[i] = new string[3];
            for (int j = 0; j < 3; j++)
            {
                data[i][j] = GetRandomJapaneseChar().ToString();
            }
        }
        // Canvasの初期化
        canvas = new Canvas
        {
            Background = Brushes.White // 背景色を白に設定
        };

        // 配列データをCanvasに描画
        UpdateCanvasContent();

        // ウィンドウのコンテンツにCanvasを設定
        win.Content = canvas;

        // ウィンドウを表示
        win.Show();
    }

    // 配列データをCanvasに追加（または更新）
    private void UpdateCanvasContent()
    {
        // Canvas内の以前の要素をクリア
        canvas.Children.Clear();

        // 各セルのサイズを指定
        double cellWidth = 50;  // セルの幅
        double cellHeight = 30; // セルの高さ

        // 配列データを表形式で描画
        for (int row = 0; row < data.Length; row++)
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                // TextBlock（データを表示するUI要素）を作成
                var textBlock = new TextBlock
                {
                    Text = data[row][col], // 配列のデータを設定
                    Foreground = Brushes.Black, // 文字色を黒に設定
                    Background = Brushes.LightGray, // 背景色を薄い灰色に設定
                    HorizontalAlignment = HorizontalAlignment.Center, // 水平方向の位置を中央に設定
                    VerticalAlignment = VerticalAlignment.Center, // 垂直方向の位置を中央に設定
                    Width = cellWidth, // セル幅
                    Height = cellHeight // セル高さ
                };

                // TextBlockの位置をCanvas上に設定
                Canvas.SetLeft(textBlock, col * cellWidth);
                Canvas.SetTop(textBlock, row * cellHeight);

                // ドラッグ＆ドロップイベントをTextBlockに追加
                textBlock.PointerPressed += OnPointerPressed; // ドラッグ開始
                textBlock.PointerMoved += OnPointerMoved;     // ドラッグ中
                textBlock.PointerReleased += OnPointerReleased; // ドロップ時

                // CanvasにTextBlockを追加
                canvas.Children.Add(textBlock);
            }
        }
    }

    // ドラッグ開始時の処理
    private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        // イベントの発生元がTextBlockである場合のみ処理
        if (sender is TextBlock textBlock)
        {
            // 現在のマウス位置を取得し、対応するセルの位置を計算
            var position = e.GetPosition(canvas);
            draggedCell = GetCellAtPosition(position); // ドラッグ中のセルの位置を記録
            draggedTextBlock = textBlock;             // ドラッグ中のTextBlockを記録
        }
    }

    // ドラッグ中の処理
    private void OnPointerMoved(object sender, PointerEventArgs e)
    {
        // ドラッグ対象が設定されていて、マウス左ボタンが押されている場合のみ処理
        if (draggedTextBlock != null && draggedCell != null && e.GetCurrentPoint(canvas).Properties.IsLeftButtonPressed)
        {
            // マウスの現在位置を取得
            var position = e.GetPosition(canvas);

            // ドラッグ中のTextBlockの表示位置をマウス位置に追随させる
            Canvas.SetLeft(draggedTextBlock, position.X - draggedTextBlock.Width / 2);
            Canvas.SetTop(draggedTextBlock, position.Y - draggedTextBlock.Height / 2);
        }
    }

    // ドロップ時の処理
    private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        // ドラッグ中のセルとTextBlockが設定されている場合のみ処理
        if (draggedCell.HasValue && draggedTextBlock != null)
        {
            // マウスの現在位置を取得し、対応するセルを計算
            var position = e.GetPosition(canvas);
            var targetCell = GetCellAtPosition(position);

            // ドラッグ元とドロップ先のセルが有効である場合のみ処理
            if (targetCell.HasValue)
            {
                // ドラッグ元とドロップ先のデータを入れ替える
                var (sourceRow, sourceCol) = draggedCell.Value;
                var (targetRow, targetCol) = targetCell.Value;

                var temp = data[sourceRow][sourceCol];
                data[sourceRow][sourceCol] = data[targetRow][targetCol];
                data[targetRow][targetCol] = temp;

                // Canvasの内容を更新して再描画
                UpdateCanvasContent();
            }
        }

        // ドラッグ状態をリセット
        draggedCell = null;
        draggedTextBlock = null;
    }

    // 指定した位置が対応するセルを計算
    private (int row, int col)? GetCellAtPosition(Avalonia.Point position)
    {
        // 各セルの幅と高さを使用して行列を計算
        double cellWidth = 50;
        double cellHeight = 30;

        int col = (int)(position.X / cellWidth); // 列を計算
        int row = (int)(position.Y / cellHeight); // 行を計算

        // 計算されたセルが配列範囲内かをチェック
        if (row >= 0 && row < data.Length && col >= 0 && col < data[row].Length)
        {
            return (row, col); // 有効なセルの場合は位置を返す
        }

        return null; // 範囲外の場合はnullを返す
    }
}
