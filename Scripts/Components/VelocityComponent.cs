using Godot;

// This MoveVelocity script is meant for Vector2 velocity.

public partial class VelocityComponent : Node, IMoveVelocity
{
    [Export]
    private float move_speed = 300;
    [Export]
    private float acc_speed = 6;
    [Export]
    private float friction = 10;

    private Vector2 velocity = new Vector2();
    private Vector2 direction = new Vector2();
    // private Vector2 direction;

    // public Vector2 SetDirection(Vector2 Direction)
    // {
    //     direction = Direction;
    //     return direction;
    // }

    public void SetDirection(float position_x, float position_y)
    {
        direction.X = position_x;
        direction.Y = position_y;
    }

    public void SetVelocity(float position_x, float position_y)
    {
        velocity.X = position_x;
        velocity.Y = position_y;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }


    public void AccelerateTo()
    {
        velocity = velocity.MoveToward(direction * move_speed, acc_speed);
    }

    public void Friction()
    {
        velocity = velocity.MoveToward(Vector2.Zero, friction);
    }

    // public void AccelerateTo(float direction_x, float direction_y)
    // {
    //     velocity.X = Mathf.MoveToward(velocity.X, direction_x * move_speed, acc_speed);
    //     velocity.Y = Mathf.MoveToward(velocity.Y, direction_y * move_speed, acc_speed);
    // }
    // public void Friction()
    // {
    //     velocity.X = Mathf.MoveToward(velocity.X, 0, friction);
    //     velocity.Y = Mathf.MoveToward(velocity.Y, 0, friction);
    // }

    public void Move(CharacterBody2D entity)
    {
        entity.Velocity = velocity;
        entity.MoveAndSlide();
    }
}