using Godot;

public partial class KeyboardInput_Component : Node
{
    public Vector2 GetDirection()
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        return direction;
    }
}
