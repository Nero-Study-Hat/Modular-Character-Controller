using Godot;

public partial class TemplateBodyState:BaseBodyState
{
    [Export]
    private BaseBodyData baseMoveData;

    public override void Enter(CharacterBody2D entity) {}

    public override void Exit(CharacterBody2D entity) {}

    public override void Process(CharacterBody2D entity) {}

    public override void PhysicsProcess(CharacterBody2D entity) {}
}