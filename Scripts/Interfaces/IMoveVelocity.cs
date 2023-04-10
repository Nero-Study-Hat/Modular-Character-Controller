using Godot;

public interface IMoveVelocity
{
    public void SetDirection(float position_x, float position_y);
    public void SetVelocity(float position_x, float position_y);

    public Vector2 GetVelocity();

    public void AccelerateTo();
    public void Friction();
}