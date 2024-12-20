using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;

class HomeContent : ContentBase
{
    public HomeContent(State state, Window window) : base(state, window) { }

    public override void Update()
    {
        UpdateContent();
    }

    public override void UpdateContent()
    {
        // ラベルの作成
        var label = new Label
        {
            Content = "W-mas",
            FontSize = 64,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = Brushes.Black // 背景と区別するための文字色
        };

        // ボタンの作成
        var startButton = new Button
        {
            Content = "Start",
            FontSize = 24,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        startButton.Click += (sender, e) =>
        {
            // ボタンがクリックされたときに状態を変更
            state.SetState(state.GAME);
        };

        // グリッドの作成
        var grid = new Grid
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        // 背景画像の設定
        var imageBrush = new ImageBrush
        {
            Source = new Bitmap("Assets/background.png"), // 画像ファイルのパスを指定
            AlignmentX = AlignmentX.Center, // 水平方向に中央揃え
            AlignmentY = AlignmentY.Center, // 垂直方向に中央揃え
        };

        // 背景画像のスケーリング
        imageBrush.Transform = new ScaleTransform
        {
            ScaleX = 1, // 横方向を80%に縮小
            ScaleY = 1, // 縦方向を80%に縮小
        };

        grid.Background = imageBrush;

        // 行定義を追加
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        // 要素をグリッドに追加
        grid.Children.Add(label);
        Grid.SetRow(label, 0); // ラベルを1行目に配置

        grid.Children.Add(startButton);
        Grid.SetRow(startButton, 1); // ボタンを2行目に配置

        // ウィンドウにコンテンツを設定
        window.Content = grid;
    }
}
