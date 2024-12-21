using Avalonia.Controls;

public class State
{
    public readonly Window window;
    private readonly HomeContent homeContent;
    private readonly GameContent gameContent;
    private readonly ResultContent resultContent;
    private int StateNumber;
    public readonly int HOME = 100;
    public readonly int GAME = 101;
    public readonly int RESULT = 102;

    private BackDrop backDrop;
    private ScoreBoard scoreBoard;
    public State(){
        window = new Window
        {
            Width = 1200,
            Height = 800,
            Title = "X-mas"
        };
        BackDrop backDrop = new();
        ScoreBoard scoreBoard = new();
        window.Show();
        homeContent = new HomeContent(this,window);
        gameContent = new GameContent(this,window,backDrop,scoreBoard);
        resultContent = new ResultContent(this,window,backDrop,scoreBoard);
        SetState(HOME);
    }

    public void SetState(int number){
        StateNumber = number;
        Update();
    }

    private void Update(){
        if (StateNumber==HOME)
        {
            homeContent.Update();
        }else if(StateNumber==GAME){
            gameContent.Update();
        }else if(StateNumber == RESULT){
            resultContent.Update();
        }
    }
}