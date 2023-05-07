using Godot;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

[Tool]
public partial class StateMachine : Node
{
    [Export]
    BaseState? _startState;

    [Export]
    private Resource _startData;

    private CharacterBody2D? _entity;

    [Export]
    BaseStateManager? _stateManager;


    public BaseState? CurrentState {get; private set;}

    public StateMachine GetStateMachine()
    {
        return this;
    }

    // [Signal]
    // public delegate void MoveState_ChangedEventHandler(BaseState MoveState);

    public void ChangeState(BaseState newState)
    {
        CurrentState.Exit();
        newState.Enter();

        CurrentState = newState;
        // EmitSignal(SignalName.MoveState_Changed, newState);
    }


    public void Init()
    {
        _entity = this.GetParent<CharacterBody2D>();

        if(_startData != null && _startData is BaseBodyData data)
        {
            _startState.SetData(data);
        }
        _startState.Enter();
        CurrentState = _startState;

        _stateManager.Initialize(this);
    }


    public void Process()
    {
        _stateManager.ConditionsChecker();
        CurrentState.Process();
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess();
    }

    // Checks if there are child nodes and if any child node is not in BodyState group.
    public override string[] _GetConfigurationWarnings()
    {
        string[] empty = new string[0];
        string[] warnings = new string[1];

        if (GetChildCount() == 0)
        {
            warnings[0] = "Assigns state nodes.";
            return warnings;
        }

        foreach (Node node in GetChildren())
        {
            if (node.IsInGroup("BodyStates") == false)
            {
                warnings[0] = "Child node is not body state.";
                return warnings;
            }
        }

        return empty;
    }
}
