using Avalonia.Controls;

public class State
{
    public readonly Window window;
    private readonly HomeContent homeContent;
    private readonly GameContent gameContent;
    private int StateNumber;
    public readonly int HOME = 100;
    public readonly int GAME = 101;
    public readonly int RESULT = 102;
    public State(){
        window = new Window();
        window.Show();
        homeContent = new HomeContent(this,window);
        gameContent = new GameContent(this,window);
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
        }
    }
}