using Godot;
using GodotUtilities;

public partial class Walk_MoveState : Base_MoveState
{
    // [Node()] // never assigned to and left as null, problem
    // private PlayerMove_KeysComponent? getDirection;

    // [Node] // assigned to just fine
    // private IMoveVelocity? moveVelocity;

    IMoveVelocity moveVelocity;
    IGetDirection getDirection;

    Vector2 direction = new Vector2();

    // [Export]
    // private IGetDirection getDirection;
    // [Export]
    // private IMoveVelocity moveVelocity;


    public override void _Ready()
    {
        for (int index = 0; index < this.GetChildCount(); index++)
        {
            if (this.GetChild(index) is IMoveVelocity)
            {
                moveVelocity = this.GetChild<IMoveVelocity>(index);
            }
            if (this.GetChild(index) is IGetDirection)
            {
                getDirection = this.GetChild<IGetDirection>(index);
            }
        }
    }


    public override void Enter(CharacterBody2D entity) {}

    public override void Exit(CharacterBody2D entity) {}


    public override void Process(CharacterBody2D entity)
    {
        moveState_Logging(entity);
    }

    public override void PhysicsProcess(CharacterBody2D entity)
    {
        direction = getDirection.GetDirection();
        moveVelocity.SetDirection(direction.X, direction.Y);
        moveVelocity.SetVelocity(entity.Velocity.X, entity.Velocity.Y);

        moveVelocity.AccelerateTo();
        moveVelocity.Friction();

        entity.Velocity = moveVelocity.GetVelocity();
        entity.MoveAndSlide();
    }


    private void moveState_Logging(CharacterBody2D entity)
    {
        string entity_velocity = GD.VarToStr(entity.Velocity);
        string entity_direction = GD.VarToStr(direction);

        using var moveState_LogFile = FileAccess.Open("user://custom_logs/velocityComponent_log.txt", FileAccess.ModeFlags.Write);
        moveState_LogFile.StoreLine("Velocity: " + entity_direction + " Direction: " + entity_direction);
    }
}
