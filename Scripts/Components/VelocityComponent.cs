using Godot;

// This MoveVelocity script is meant for Vector2 velocity.

public partial class VelocityComponent : Node, IMoveVelocity
{
    private float moveSpeedBase = 1f;
    private float moveSpeedAcc = 1f;
    private float MoveSpeedFric = 1f;

    private Vector2 velocity;
    private Vector2 direction;


    public void Initialize_MoveSpeedData(float MoveSpeedBase, float MoveSpeedAcceleration, float MoveSpeedFriction)
    {
        moveSpeedBase = MoveSpeedBase;
        moveSpeedAcc = MoveSpeedAcceleration;
        MoveSpeedFric = MoveSpeedFriction;
    }

    public void SetDirection(float position_x, float position_y)
    {
        direction = new Vector2(position_x, position_y);
    }

    public void SetVelocity(float position_x, float position_y)
    {
        velocity = new Vector2(position_x, position_y);
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }


    public void AccelerateTo()
    {
        velocity = velocity.MoveToward(direction * moveSpeedBase, moveSpeedAcc);
    }

    public void Friction() // this is giving me my movement bug
    {
        velocity = velocity.MoveToward(Vector2.Zero, MoveSpeedFric);
    }


    public void Move(CharacterBody2D entity)
    {
        entity.Velocity = velocity;
        entity.MoveAndSlide();
    }
}