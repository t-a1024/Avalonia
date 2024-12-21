using Avalonia.Controls;

public abstract class ContentBase
{
    protected State state;
    protected Window window;

    protected ContentBase(State state, Window window)
    {
        this.state = state;
        this.window = window;
    }

    public abstract void UpdateContent();
    public abstract void Update();
}
