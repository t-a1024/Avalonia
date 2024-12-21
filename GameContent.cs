using Avalonia.Controls;

class GameContent(State state, Window window, BackDrop backDrop, ScoreBoard scoreBoard) : ContentBase(state, window)
{
    private readonly BackDrop backDrop = backDrop;
    private readonly ScoreBoard scoreBoard = scoreBoard;

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
