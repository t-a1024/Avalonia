using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

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
            Background = Brushes.AntiqueWhite,
        };

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
