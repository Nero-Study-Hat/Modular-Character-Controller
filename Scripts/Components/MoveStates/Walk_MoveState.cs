using Godot;
using GodotUtilities;

public partial class Walk_MoveState : Base_MoveState
{
    // [Node()] // never assigned to and left as null, problem
    // private PlayerMove_KeysComponent? getDirection;

    // [Node] // assigned to just fine
    // private IMoveVelocity? moveVelocity;

    IMoveVelocity moveVelocity;
    IGetDirection getDirection;

    Vector2 direction = new Vector2();

    // [Export]
    // private IGetDirection getDirection;
    // [Export]
    // private IMoveVelocity moveVelocity;


    public override void _Ready()
    {
        // newLogFile();
        new_LogFile();

        for (int index = 0; index < this.GetChildCount(); index++)
        {
            if (this.GetChild(index) is IMoveVelocity)
            {
                moveVelocity = this.GetChild<IMoveVelocity>(index);
            }
            if (this.GetChild(index) is IGetDirection)
            {
                getDirection = this.GetChild<IGetDirection>(index);
            }
        }
    }


    public override void Enter(CharacterBody2D entity) {}

    public override void Exit(CharacterBody2D entity) {}


    public override void Process(CharacterBody2D entity)
    {
        // moveState_Logging(entity);
        update_LogFile(entity);
    }

    public override void PhysicsProcess(CharacterBody2D entity)
    {
        // Direction is working but entity.Velocity is never updating.

        direction = getDirection.GetDirection();
        moveVelocity.SetDirection(direction.X, direction.Y);
        moveVelocity.SetVelocity(entity.Velocity.X, entity.Velocity.Y);

        moveVelocity.AccelerateTo();
        moveVelocity.Friction();

        entity.Velocity = moveVelocity.GetVelocity();
        entity.MoveAndSlide();
    }


// These two functions are currently useless as I am unable to append a new line at the end of the log file.
    // private void newLogFile()
    // {
    //     using var moveState_LogFile = FileAccess.Open("user://custom_logs/walkState_log.txt", FileAccess.ModeFlags.Write);
    // }

    // private void moveState_Logging(CharacterBody2D entity)
    // {
    //     string entity_velocity = GD.VarToStr(entity.Velocity);
    //     string entity_direction = GD.VarToStr(direction);

    //     using var moveState_LogFile = FileAccess.Open("user://custom_logs/walkState_log.txt", FileAccess.ModeFlags.ReadWrite);
    //     moveState_LogFile.StoreString("\n");
    //     moveState_LogFile.StoreString("Velocity: " + entity_direction + " Direction: " + entity_direction);
    // }

// This works but I copying and rewriting the entire file every frame which is a horrible idea.
    public void new_LogFile()
    {
        using var file = FileAccess.Open("user://custom_logs/log_file.txt", FileAccess.ModeFlags.Write);
        file.StoreString("Hello world.");
    }

    public void update_LogFile(CharacterBody2D entity)
    {
        string entity_velocity = GD.VarToStr(entity.Velocity);
        string entity_direction = GD.VarToStr(direction);

        using var file = FileAccess.Open("user://custom_logs/log_file.txt", FileAccess.ModeFlags.ReadWrite);
        string content = file.GetAsText() + "\n" + "Velocity: " + entity_velocity + " Direction: " + entity_direction;
        file.StoreString(content);
    }
}
