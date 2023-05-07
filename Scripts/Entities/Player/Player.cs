using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	private StateMachine _stateMachine = new StateMachine();

// in the future maybe setup a log for who is accessing this if needed
	public Player GetPlayerRef()
	{
		return this;
	}

    public override void _Ready()
    {
        _stateMachine.Init();
    }

	public override void _Process(double delta)
    {
        _stateMachine.Process();
    }
	
    public override void _PhysicsProcess(double delta)
    {
        _stateMachine.PhysicsProcess();
    }
}
