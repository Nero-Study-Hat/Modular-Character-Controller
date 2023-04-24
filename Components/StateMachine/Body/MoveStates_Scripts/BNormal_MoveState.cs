using Godot;
using System;

partial class BNormal_MoveState : BaseBodyState
{
    IMoveVelocity moveVelocity;
    IGetDirection getDirection;

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


    public override void Enter(CharacterBody2D entity, BaseMoveData[] stateData)
    {
        foreach (BaseMoveData dataRes in stateData)
        {
            if (dataRes is BNormal_Data bNormal_Data)
            {
                moveVelocity.Initialize_MoveSpeedData(bNormal_Data.MoveSpeedBase, bNormal_Data.MoveSpeedAcceleration, bNormal_Data.MoveSpeedFriction);
            }
        }
    }


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

    public override void Exit(CharacterBody2D entity) {}
}
