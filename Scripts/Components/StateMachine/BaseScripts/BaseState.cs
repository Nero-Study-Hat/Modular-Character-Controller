using Godot;


public abstract partial class BaseState : Node
{
    public abstract void Enter();

    public virtual void SetResource(BaseBodyData stateData) {} // TODO Turn into a generic method with the constraint of being a resource.

    public abstract void Exit();

    public abstract void Process();

    public abstract void PhysicsProcess();
}