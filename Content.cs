using Avalonia.Controls;

abstract class ContentBase(State state, Window window)
{
    protected State state = state;
    protected Window window = window;

    public abstract void UpdateContent();
    public abstract void Update();
}
