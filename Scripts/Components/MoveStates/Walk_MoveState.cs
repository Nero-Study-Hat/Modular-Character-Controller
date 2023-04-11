using Godot;
using GodotUtilities;

public partial class Walk_MoveState : Base_MoveState
{
    // [Node()] // never assigned to and left as null, problem
    // private PlayerMove_KeysComponent? getDirectionComponent;

    // [Node] // assigned to just fine
    // private IMoveVelocity? moveVelocityComponent;

    Vector2 direction = new Vector2();

    public override void _Notification(int what)
    {
        base._Notification(what); // needed for IMoveVelocity node assignment
        
        if (what == NotificationSceneInstantiated)
        {
            this.WireNodes();
        }
    }

    // [Export]
    // private IGetDirection getDirectionComponent;
    // [Export]
    // private IMoveVelocity moveVelocityComponent;

    public override void Enter(CharacterBody2D entity) {}

    public override void Exit(CharacterBody2D entity) {}


    public override void Process(CharacterBody2D entity) {}

    public override void PhysicsProcess(CharacterBody2D entity)
    {
        direction = getDirectionComponent.GetDirection();
        moveVelocityComponent.SetDirection(direction.X, direction.Y);
        moveVelocityComponent.SetVelocity(entity.Velocity.X, entity.Velocity.Y);

        moveVelocityComponent.AccelerateTo();
        moveVelocityComponent.Friction();

        entity.Velocity = moveVelocityComponent.GetVelocity();
        entity.MoveAndSlide();
    }
}
