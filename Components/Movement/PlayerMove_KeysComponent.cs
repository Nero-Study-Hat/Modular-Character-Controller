using Godot;

public partial class PlayerMove_KeysComponent : Node, IGetDirection
{
	public Vector2 GetDirection()
    {
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        return direction;
    }
}
