using System;
using Avalonia.Controls;
using Avalonia.Media;

public class ScoreBoard
{
    private int count = 0;
    private int score = 0;
    private int combo = 0;
    private Canvas canvas;
    private readonly int CanvasWidth = 600;
    private readonly int CanvasHeight = 600;

    private IBrush BackgroundBrush { get; set; } = new SolidColorBrush(Colors.CornflowerBlue, 0.5);
    private double FontSize { get; set; } = 24;

    public ScoreBoard()
    {
    }

    private void UpdateCanvas()
    {
        if (canvas == null)
        {
            canvas = new Canvas
            {
                Background = BackgroundBrush,
                Width = CanvasWidth,
                Height = CanvasHeight
            };
        }
        else
        {
            canvas.Children.Clear();
        }

        // スコア表示
        var scoreText = new TextBlock
        {
            Text = $"Score: {score}",
            FontSize = FontSize,
            Foreground = Brushes.White
        };
        Canvas.SetLeft(scoreText, 20);
        Canvas.SetTop(scoreText, 20);
        canvas.Children.Add(scoreText);

        // スコア表示
        var countText = new TextBlock
        {
            Text = $"残り: {count}",
            FontSize = FontSize,
            Foreground = Brushes.White
        };
        Canvas.SetLeft(countText, 20);
        Canvas.SetTop(countText, 60);
        canvas.Children.Add(countText);

        // コンボ表示
        var comboText = new TextBlock
        {
            Text = $"Combo: {combo}",
            FontSize = FontSize,
            Foreground = Brushes.White
        };
        Canvas.SetLeft(comboText, 20);
        Canvas.SetTop(comboText, 100);
        canvas.Children.Add(comboText);
    }

    public Canvas ScoreCanvas()
    {
        UpdateCanvas();
        return canvas;
    }

    public void AddScore(){
        score += 100 * combo;
    }
    public void UpdateScore(int points)
    {
        score += points;
        UpdateCanvas();
    }

    public void AddCombo(){
        combo++;
        UpdateCanvas();
        Console.WriteLine(combo);
    }

    public void ResetCombo()
    {
        combo = 0;
        UpdateCanvas();
    }

    public void SetCount(int count){
        this.count = count;
    }

    public void Reset()
    {
        score = 0;
        combo = 0;
        UpdateCanvas();
    }
    public int GetScore(){
        return score;
    }
}
