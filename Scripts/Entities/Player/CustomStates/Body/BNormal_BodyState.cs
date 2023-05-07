using Godot;
using System.Linq;
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
        var velocityNodeDependency = this.GetChildren().Where(node => node is IMoveVelocity).ToList();
        var directionNodeDependency = this.GetChildren().Where(node => node is IGetDirection).ToList();
        if (velocityNodeDependency.Count != 1)
        {
            GD.PrintErr($"Incorrect amount of {moveVelocity.GetType} nodes in: {this}");
        }
        if (directionNodeDependency.Count != 1)
        {
            GD.PrintErr($"Incorrect amount of {getDirection.GetType} nodes in: {this}");
        }
        moveVelocity = (IMoveVelocity)velocityNodeDependency[0];
        getDirection = (IGetDirection)directionNodeDependency[0];
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
