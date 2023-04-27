using Godot;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class BodyStateMachine : Node
{
    [Export]
    BaseBodyState startState;

    private CharacterBody2D entity;
    private BaseMoveData[] statesResources;

    [Export]
    BaseBodyStateManager conditionsCheck;

    public BaseBodyState CurrentState {get; private set;}

    public BodyStateMachine GetBody_StateMachine()
    {
        return this;
    }

    [Signal]
    public delegate void MoveState_ChangedEventHandler(BaseBodyState MoveState);

    public void ChangeState(BaseBodyState newState)
    {
        CurrentState.Exit(entity);
        newState.Enter(entity, statesResources);

        CurrentState = newState;
        EmitSignal(SignalName.MoveState_Changed, newState);
    }


    public void Init(BaseMoveData[] StatesResources)
    {
        entity = this.GetParent<CharacterBody2D>();

        statesResources = StatesResources;

        startState.Enter(entity, statesResources);
        CurrentState = startState;

        conditionsCheck.Initialize(entity, this);
    }


    public void Process()
    {
        conditionsCheck.ConditionsChecker();
        CurrentState.Process(entity);
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess(entity);
    }
}
