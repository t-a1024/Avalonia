using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia;

class ResultContent(State state, Window window, BackDrop backDrop, ScoreBoard scoreBoard) : ContentBase(state, window)
{
    private readonly BackDrop backDrop = backDrop;
    private readonly ScoreBoard scoreBoard = scoreBoard;
    private Canvas canvas;

    public override void Update()
    {
        UpdateContent();
    }

    public override void UpdateContent()
    {
        // ラベルの作成
        var label = new Label
        {
            Content = "Score: " + scoreBoard.GetScore(),
            FontSize = 64,
            Foreground = Brushes.Black, // 背景と区別するための文字色
            Width = 600,
            HorizontalContentAlignment = HorizontalAlignment.Center, // 文字の中央揃え
            VerticalContentAlignment = VerticalAlignment.Center // 文字の中央揃え
        };

        // ボタンの作成
        var returnButton = new Button
        {
            Content = "ホームへ",
            FontSize = 24,
            Width = 200,
            Height = 100,
            HorizontalContentAlignment = HorizontalAlignment.Center, // 文字の中央揃え
            VerticalContentAlignment = VerticalAlignment.Center // 文字の中央揃え
        };

        returnButton.Click += (sender, e) =>
        {
            // ボタンがクリックされたときに状態を変更
            ReturnHome();
        };


        // Canvasの作成
        canvas = new Canvas
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        // ウィンドウのサイズに応じてラベルの位置を中央に配置
        var labelWidth = label.Bounds.Width;
        var labelHeight = label.Bounds.Height;
        

        // ラベルの位置をCanvasの中央に配置
        Canvas.SetLeft(label, (1200 - 600) / 2);
        Canvas.SetTop(label, 200);

        // ボタンの位置を設定
        returnButton.Margin = new Thickness(0, 0, 0, 20);  // 下に余白を作る
        Canvas.SetLeft(returnButton, (1200 - 200) / 2);
        Canvas.SetTop(returnButton, window.Height * 2 / 3);

        //背景を設定
        Canvas.SetLeft(backDrop,0);
        Canvas.SetTop(backDrop,0);

        // 要素をCanvasに追加
        canvas.Children.Add(backDrop);
        canvas.Children.Add(label);
        canvas.Children.Add(returnButton);

        // ウィンドウにコンテンツを設定
        window.Content = canvas;
    }
    private void ReturnHome(){
        canvas.Children.Clear();
        state.SetState(state.HOME);
    }
}
