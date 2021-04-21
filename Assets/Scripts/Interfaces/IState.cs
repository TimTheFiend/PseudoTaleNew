// This interface is used for easy switching between player states, such as Movement, Interact, and Menu.
// All classes that implements this method must have `Enter`, `Execute`, and `Exit` which will be called in that order.
public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}
