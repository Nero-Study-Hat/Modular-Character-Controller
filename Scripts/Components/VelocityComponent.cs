using Godot;

public partial class VelocityComponent : Node
{
    [Export]
    private float move_speed = 300;
    [Export]
    private float acc_speed = 6;
    [Export]
    private float friction = 10;

    private Vector2 velocity = new Vector2();
    

    public void Accelerate(Vector2 direction)
    {
        velocity = velocity.MoveToward(direction * move_speed, acc_speed);
    }
    public void Friction()
    {
        velocity = velocity.MoveToward(Vector2.Zero, friction);
    }

    public void Move(CharacterBody2D characterBody2D)
    {
        characterBody2D.Velocity = velocity;
        characterBody2D.MoveAndSlide();
    }
}