using Godot;
using System;

public partial class KeyboardInput_Component : Node, ISetDirection
{
    public Vector2 SetDirection()
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        return direction;
    }
}
