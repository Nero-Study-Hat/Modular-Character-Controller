using Godot;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public abstract partial class Base_MoveState : Node
{
    // [Export]
    // protected PhysicsBody2D entity;

    public abstract void Enter(CharacterBody2D entity);

    public abstract void Exit(CharacterBody2D entity);

    public abstract void Process(CharacterBody2D entity);

    public abstract void PhysicsProcess(CharacterBody2D entity);
}