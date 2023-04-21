using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private Movement_StateMachine moveStatesManager = new Movement_StateMachine();

// in the future maybe setup a log for who is accessing this if needed
	public Player GetPlayerRef()
	{
		return this;
	}

    public override void _Ready()
    {
        moveStatesManager.Init();
    }

	public override void _UnhandledInput(InputEvent @event)
    {
        //
    }

	public override void _Process(double delta)
    {
        moveStatesManager.Process();
    }
	
    public override void _PhysicsProcess(double delta)
    {
        moveStatesManager.PhysicsProcess();
    }
}
