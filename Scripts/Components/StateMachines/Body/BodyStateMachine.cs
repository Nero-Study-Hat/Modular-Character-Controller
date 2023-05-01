using Godot;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

[Tool]
public partial class BodyStateMachine : Node
{
    [Export]
    BaseBodyState _startState;

    private CharacterBody2D _entity;
    private BaseMoveData[] _statesResources;

    [Export]
    BaseBodyStateManager _conditionsCheck;

    public BaseBodyState CurrentState {get; private set;}

    public BodyStateMachine GetBody_StateMachine()
    {
        return this;
    }

    [Signal]
    public delegate void MoveState_ChangedEventHandler(BaseBodyState MoveState);

    public void ChangeState(BaseBodyState newState)
    {
        CurrentState.Exit(_entity);
        newState.Enter(_entity, _statesResources);

        CurrentState = newState;
        EmitSignal(SignalName.MoveState_Changed, newState);
    }


    public void Init(BaseMoveData[] StatesResources)
    {
        _entity = this.GetParent<CharacterBody2D>();

        _statesResources = StatesResources;

        _startState.Enter(_entity, _statesResources);
        CurrentState = _startState;

        _conditionsCheck.Initialize(_entity, this);
    }


    public void Process()
    {
        _conditionsCheck.ConditionsChecker();
        CurrentState.Process(_entity);
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess(_entity);
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
