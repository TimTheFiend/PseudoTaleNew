public class StateMachine
{
    private IState currentState;

    // Handles exiting, changing, and entering different States
    public void ChangeState(IState newState) {
        if (currentState != null) {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter();
    }

    public void Update() {
        if (currentState != null) { currentState.Execute(); }
    }
}
