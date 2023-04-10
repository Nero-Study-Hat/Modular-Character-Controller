using Godot;
using GodotUtilities;

public partial class Walk_MoveState : Base_MoveState
{
    // [Node]
    // private KeyboardInput_Component keyboardInput_Component;

// Problem is having a big component file that implements
// only a select function. This would mean I'd need to get
// the name component node a bunch of times for different times.
    [Node]
    private ISetDirection setDirectionComponent;
    [Node]
    private IMoveVelocity moveVelocityComponent;

    public override void _EnterTree()
    {
        this.WireNodes();
    }

    public override void Enter(CharacterBody2D entity) {}

    public override void Exit(CharacterBody2D entity) {}


    public override void Process(CharacterBody2D entity) {}

    public override void PhysicsProcess(CharacterBody2D entity)
    {
        var direction = setDirectionComponent.SetDirection();
        moveVelocityComponent.SetDirection(direction.X, direction.Y);
        moveVelocityComponent.SetVelocity(entity.Velocity.X, entity.Velocity.Y);

        moveVelocityComponent.AccelerateTo();
        moveVelocityComponent.Friction();

        entity.Velocity = moveVelocityComponent.GetVelocity();
        entity.MoveAndSlide();
    }
}
