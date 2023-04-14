using Godot;
using System;

public partial class Walk_MoveState : Base_MoveState
{
    // [Node()] // never assigned to and left as null, problem
    // private PlayerMove_KeysComponent? getDirection;

    // [Node] // assigned to just fine
    // private IMoveVelocity? moveVelocity;

    IMoveVelocity moveVelocity;
    IGetDirection getDirection;

    Vector2 direction = new Vector2();

    string logFileDir;
    string logFileName;

    // [Export]
    // private IGetDirection getDirection;
    // [Export]
    // private IMoveVelocity moveVelocity;


    public override void _Ready()
    {
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


    private void new_LogFile()
    {
        string currentTime = DateTime.Now.ToString("MMM_dd_HHmm");
        string scriptName = this.GetScript().ToString();

        string currentFile = this.GetScript().GetType().ToString();

        logFileDir = "user://custom_logs/";
        logFileName = currentFile + "_Log_" + currentTime + ".txt";
        using var moveState_LogFile = FileAccess.Open(logFileDir + logFileName, FileAccess.ModeFlags.Write);
    }

    private void update_LogFile(CharacterBody2D entity)
    {
        string entity_velocity = GD.VarToStr(entity.Velocity);
        string entity_direction = GD.VarToStr(direction);

        using var moveState_LogFile = FileAccess.Open(logFileName, FileAccess.ModeFlags.ReadWrite);
        moveState_LogFile.SeekEnd();
        moveState_LogFile.StoreString("\n" + "Velocity: " + entity_velocity + " Direction: " + entity_direction);
    }
}
