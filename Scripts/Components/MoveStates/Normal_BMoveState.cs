using Godot;
using System;

public partial class Normal_BMoveState : Base_MoveState
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


    public override void Process(CharacterBody2D entity) {}

    public override void PhysicsProcess(CharacterBody2D entity)
    {
        // Direction is working but entity.Velocity is never updating.

        direction = getDirection.GetDirection();
        moveVelocity.SetDirection(direction.X, direction.Y);
        moveVelocity.SetVelocity(entity.Velocity.X, entity.Velocity.Y);

        if (direction != Vector2.Zero)
        {
            moveVelocity.AccelerateTo();
        }
        else
        {
            moveVelocity.Friction();
        }

        entity.Velocity = moveVelocity.GetVelocity();
        entity.MoveAndSlide();
    }
}
