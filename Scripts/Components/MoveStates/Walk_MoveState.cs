using Godot;
using GodotUtilities;

public partial class Walk_MoveState : Base_MoveState
{
    [Node]
    private KeyboardInput_Component keyboardInput_Component;
    [Node]
    private VelocityComponent velocityComponent;

    public override void _EnterTree()
    {
        this.WireNodes();
    }

    public override void Enter(CharacterBody2D entity) {}

    public override void Exit(CharacterBody2D entity) {}


    public override void Process(CharacterBody2D entity) {}

    public override void PhysicsProcess(CharacterBody2D entity)
    {
        Vector2 direction = keyboardInput_Component.GetDirection();
        velocityComponent.Accelerate(direction);
        velocityComponent.Friction();
        velocityComponent.Move(entity);
    }
}
