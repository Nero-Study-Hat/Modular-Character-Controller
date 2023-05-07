using Godot;
using System.Linq;
using System.Diagnostics;

partial class BNormal_BodyState : BaseState
{
    CharacterBody2D _entity = new CharacterBody2D();
    private BNormal_Data _data;

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

    public override void SetResource<BNormal_Data>(BNormal_Data resource)
    {
        if(resource is BNormal_Data)
        {
            _data = resource; // FIXME Can't convert BNormal_Data to BNormal_Data. HELP !!!
            var moveData = new BodyStateData.MoveData();

            moveData.MoveSpeedBase = _data.MoveSpeedBase;
            moveData.MoveSpeedAcceleration = _data.MoveSpeedAcceleration;
            moveData.MoveSpeedFriction = _data.MoveSpeedFriction;
            moveVelocity.Initialize_MoveSpeedData(moveData);
        }
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
