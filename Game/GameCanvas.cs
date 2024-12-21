using Avalonia.Controls;
using Avalonia.Layout;

public class GameCanvas : Canvas
{
    public GameCanvas()
    {
        // 親Canvasの設定
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;

        var backDrop = new BackDrop();

        // Tableのインスタンスを作成し、TableCanvas()を呼び出してCanvasを取得
        GameTable gameTable = new GameTable(backDrop);
        var tableCanvas = gameTable.TableCanvas();

        // GameCanvasに追加
        Children.Add(backDrop);  // 先に追加して背景として配置
        Children.Add(tableCanvas); // 次にtableCanvasを追加

        // backDropの位置を設定
        SetLeft(backDrop, 0);  // GameCanvas内の位置
        SetTop(backDrop, 0);   // GameCanvas内の位置

        // TableCanvasの位置を設定
        SetLeft(tableCanvas, 100);  // GameCanvas内の位置
        SetTop(tableCanvas, 100);   // GameCanvas内の位置
    }
}
