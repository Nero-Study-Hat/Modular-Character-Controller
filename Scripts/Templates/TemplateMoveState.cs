using Godot;

public partial class TemplateMoveState:Base_MoveState
{
    [Export]
    private BaseMoveData baseMoveData;

    public override void Enter(CharacterBody2D entity, BaseMoveData[] stateData) {}

    public override void Exit(CharacterBody2D entity) {}

    public override void Process(CharacterBody2D entity) {}

    public override void PhysicsProcess(CharacterBody2D entity) {}
}