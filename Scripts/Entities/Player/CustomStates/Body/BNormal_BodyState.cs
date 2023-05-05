using Godot;
using System.Diagnostics;

partial class BNormal_BodyState : BaseBodyState
{
    private BNormal_Data _data;

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

    public override void SetResource(BaseBodyData stateData)
    {
        if(stateData is BNormal_Data bNormal_Data)
        {
            _data = bNormal_Data;
            var moveData = new BodyStateData.MoveData();

            moveData.MoveSpeedBase = _data.MoveSpeedBase;
            moveData.MoveSpeedAcceleration = _data.MoveSpeedAcceleration;
            moveData.MoveSpeedFriction = _data.MoveSpeedFriction;
            moveVelocity.Initialize_MoveSpeedData(moveData);
        }
    }

    public override void Enter(CharacterBody2D entity) {}


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
