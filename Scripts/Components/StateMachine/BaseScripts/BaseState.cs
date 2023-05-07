using Godot;


public abstract partial class BaseState : Node
{
    public abstract void Enter();

    public virtual void SetData(Resource stateData) {}

    public abstract void Exit();

    public abstract void Process();

    public abstract void PhysicsProcess();
}