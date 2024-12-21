using Avalonia.Controls;
using Avalonia.Layout;

public class GameCanvas : Canvas
{
    public GameCanvas()
    {
        // 親Canvasの設定
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;

        BackDrop backDrop = new();
        ScoreBoard scoreBoard = new();
        Canvas scoreCanvas = scoreBoard.ScoreCanvas();
        

        // Tableのインスタンスを作成し、TableCanvas()を呼び出してCanvasを取得
        GameTable gameTable = new GameTable(backDrop,scoreBoard);
        var tableCanvas = gameTable.TableCanvas();

        // GameCanvasに追加
        Children.Add(backDrop);  // 先に追加して背景として配置
        Children.Add(tableCanvas); // 次にtableCanvasを追加
        Children.Add(scoreCanvas);

        // backDropの位置を設定
        SetLeft(backDrop, 0);  // GameCanvas内の位置
        SetTop(backDrop, 0);   // GameCanvas内の位置

        // TableCanvasの位置を設定
        SetLeft(tableCanvas, 100);  // GameCanvas内の位置
        SetTop(tableCanvas, 100);   // GameCanvas内の位置

        // ScoreBoradの位置を設定
        SetRight(scoreCanvas, 0);  // GameCanvas内の位置
        SetTop(scoreCanvas, 0);   // GameCanvas内の位置
    }
}
