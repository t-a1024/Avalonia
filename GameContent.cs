using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

class GameContent : ContentBase
{
    private BackDrop backDrop;
    private ScoreBoard scoreBoard;
    public GameContent(State state, Window window, BackDrop backDrop, ScoreBoard scoreBoard) : base(state, window)
    { 
        this.backDrop = backDrop;
        this.scoreBoard = scoreBoard;
    }

    public override void Update()
    {
        UpdateContent();
    }

    public override void UpdateContent()
    {
        // Canvasをウィンドウのコンテンツに設定
        window.Content = new GameCanvas(state,backDrop,scoreBoard);
    }
}
