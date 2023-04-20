using Godot;
using System;

partial class BNormal_MS : Base_MoveState
{
    IMoveVelocity moveVelocity;
    IGetDirection getDirection;

    ISwitchMoveStates_Check switchMoveStates_Check;
    ISpawnMoveStates_Check spawnMoveStates_Check;

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

    // Initilization -> Resource & Script Dependencies
    public override void Enter(CharacterBody2D entity)
    {
        // connect up conditions script dependencies
        InitializeCheckDependencies(entity, this, switchMoveStates_Check, spawnMoveStates_Check);

        switchMoveStates_Check.Initialize(entity, this.GetOwner<Movement_StateMachine>());

        // resource values setup
        moveVelocity.Initialize_MoveSpeedData(normal_MoveStateData.MoveSpeedBase, normal_MoveStateData.MoveSpeedAcceleration, normal_MoveStateData.MoveSpeedFriction);
    }


    public override void Process(CharacterBody2D entity) 
    {
        switchMoveStates_Check.ConditionsChecker();
    }

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
