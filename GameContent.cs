using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

class GameContent : ContentBase
{
    public GameContent(State state, Window window) : base(state, window){  }

    public override void Update()
    {
        UpdateContent();
    }

    public override void UpdateContent()
    {
        // Canvasをウィンドウのコンテンツに設定
        window.Content = new GameCanvas();
    }
}
