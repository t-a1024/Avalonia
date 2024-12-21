using System;
using Avalonia.Controls;
using Avalonia.Layout;

public class GameCanvas : Canvas
{
    private State state;
    public GameCanvas(State state,BackDrop backDrop,ScoreBoard scoreBoard)
    {
        this.state = state;
        // 親Canvasの設定
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;

        Canvas scoreCanvas = scoreBoard.ScoreCanvas();
        
        // Tableのインスタンスを作成し、TableCanvas()を呼び出してCanvasを取得
        GameTable gameTable = new GameTable(backDrop,scoreBoard,FinishGame);
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
        SetRight(scoreCanvas, 50);  // GameCanvas内の位置
        SetTop(scoreCanvas, 100);   // GameCanvas内の位置
    }
    public void FinishGame(){
        Console.WriteLine("Finish");
        state.SetState(state.RESULT);
    }
}
