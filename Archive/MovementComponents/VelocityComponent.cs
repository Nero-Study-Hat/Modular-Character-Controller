using Godot;

// This MoveVelocity script is meant for Vector2 velocity.

public partial class VelocityComponent : Node, IMoveVelocity
{
    private BodyStateData.MoveData _moveData;

    private Vector2 velocity;
    private Vector2 direction;


    public void Initialize_MoveSpeedData(BodyStateData.MoveData moveData)
    {
        _moveData = moveData;
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
        velocity = velocity.MoveToward(direction * _moveData.MoveSpeedBase, _moveData.MoveSpeedAcceleration);
    }

    public void Friction() // this is giving me my movement bug
    {
        velocity = velocity.MoveToward(Vector2.Zero, _moveData.MoveSpeedFriction);
    }


    public void Move(CharacterBody2D entity)
    {
        entity.Velocity = velocity;
        entity.MoveAndSlide();
    }
}