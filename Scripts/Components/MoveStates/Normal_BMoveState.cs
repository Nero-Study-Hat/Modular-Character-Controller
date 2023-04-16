using Godot;
using System;

public partial class Normal_BMoveState : Base_MoveState
{
    IMoveVelocity moveVelocity;
    IGetDirection getDirection;

    [Export]
    private TestShip1Data normal_MoveStateData;

    Vector2 direction = new Vector2();


    // Connect up component nodes.
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

    // Assign data from resource file(s) here.
    public override void Enter(CharacterBody2D entity)
    {
        moveVelocity.Initialize_MoveSpeedData(normal_MoveStateData.MoveSpeedBase, normal_MoveStateData.MoveSpeedAcceleration, normal_MoveStateData.MoveSpeedFriction);
    }

    public override void Exit(CharacterBody2D entity) {}


    public override void Process(CharacterBody2D entity) {}

    // Handle direction and velocity with MoveAndSlide.
    public override void PhysicsProcess(CharacterBody2D entity)
    {
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
