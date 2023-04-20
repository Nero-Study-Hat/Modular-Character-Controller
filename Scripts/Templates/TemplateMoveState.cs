using Godot;

public partial class TemplateMoveState:Base_MoveState
{
    Movement_StateMachine stateMachine;

    ISwitchMoveStates_Check switchMoveStates_Check;
    ISpawnMoveStates_Check spawnMoveStates_Check;

    [Export]
    private BaseMoveData baseMoveData;

    public override void Enter(CharacterBody2D entity)
    {
        InitializeCheckDependencies(entity, this, switchMoveStates_Check, spawnMoveStates_Check);
        switchMoveStates_Check.Initialize(entity, stateMachine);
    }

    public override void Exit(CharacterBody2D entity) {}

    public override void Process(CharacterBody2D entity)
    {
        switchMoveStates_Check.ConditionsChecker();
    }

    public override void PhysicsProcess(CharacterBody2D entity) {}
}