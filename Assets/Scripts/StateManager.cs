using System.Collections.Generic;
using System.Linq;

public class StateManager
{
    private readonly List<IStateUi> states = new List<IStateUi>();

    public IStateUi CurrentState { get; private set; }

    public void GoToState<T>() where T : IStateUi
    {
        var newState = states.FirstOrDefault(x => x is T);
        if (CurrentState == newState)
            return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void GoToState(IStateUi newState)
    {
        CurrentState?.Exit();

        if (CurrentState == newState)
            return;

        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void AddState<T>(T state) where T : IStateUi
    {
        states.Add(state);
    }
}