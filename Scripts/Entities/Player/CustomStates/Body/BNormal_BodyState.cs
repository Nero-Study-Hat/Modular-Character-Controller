using Godot;
using System.Linq;
using System.Diagnostics;

partial class BNormal_BodyState : BaseState
{
    CharacterBody2D _entity = new CharacterBody2D();

    IMoveVelocity moveVelocity;
    IGetDirection getDirection;

    Vector2 direction = new Vector2();

    // Connect up component nodes.
    public override void _Ready()
    {
        _entity = this.GetOwner<CharacterBody2D>();
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

    public override void SetData(Resource stateData)
    {
        if(stateData is not BNormal_Data bNormal_Data)
        {
            return;
        }

        var moveData = new BodyStateData.MoveData();
        moveData.MoveSpeedBase = bNormal_Data.MoveSpeedBase;
        moveData.MoveSpeedAcceleration = bNormal_Data.MoveSpeedAcceleration;
        moveData.MoveSpeedFriction = bNormal_Data.MoveSpeedFriction;
        moveVelocity.Initialize_MoveSpeedData(moveData);
    }

    public override void Enter() {}


    public override void Process() {}

    // Handle direction and velocity with MoveAndSlide.
    public override void PhysicsProcess()
    {
        direction = getDirection.GetDirection();
        moveVelocity.SetDirection(direction.X, direction.Y);
        moveVelocity.SetVelocity(_entity.Velocity.X, _entity.Velocity.Y);

        if (direction != Vector2.Zero)
        {
            moveVelocity.AccelerateTo();
        }
        else
        {
            moveVelocity.Friction();
        }

        _entity.Velocity = moveVelocity.GetVelocity();
        _entity.MoveAndSlide();
    }

    public override void Exit() {}
}
