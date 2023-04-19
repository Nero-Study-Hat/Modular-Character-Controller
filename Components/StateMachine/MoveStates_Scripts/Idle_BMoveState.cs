using Godot;
using System;

public partial class Idle_BMoveState : Base_MoveState
{
    // [Node]
    // private VelocityComponent velocityComponent;

    // public override void _EnterTree()
    // {
    //     this.WireNodes();
    // }


    public override void Enter(CharacterBody2D entity) {}

    public override void Exit(CharacterBody2D entity) {}


    public override void Process(CharacterBody2D entity) {}

    public override void PhysicsProcess(CharacterBody2D entity)
    {
        //
    }
}
