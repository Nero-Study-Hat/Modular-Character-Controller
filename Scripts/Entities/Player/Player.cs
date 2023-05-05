using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private BodyStateMachine _bodyStateMachine = new BodyStateMachine();

// in the future maybe setup a log for who is accessing this if needed
	public Player GetPlayerRef()
	{
		return this;
	}

    public override void _Ready()
    {
        _bodyStateMachine.Init();
    }

	public override void _Process(double delta)
    {
        _bodyStateMachine.Process();
    }
	
    public override void _PhysicsProcess(double delta)
    {
        _bodyStateMachine.PhysicsProcess();
    }
}
