using Godot;
using System;

public partial class PlayerMove_KeysComponent : Node, ISetDirection
{
	// [Export]
	// private VelocityComponent velocityComponent;

	// public void PhysicsProcess(double delta)
	// {
	// 	var direction = SetDirection();
	// 	velocityComponent.SetDirection(direction);
	// }

	public Vector2 SetDirection()
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        return direction;
    }
}
