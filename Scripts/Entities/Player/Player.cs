using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private Movement_StateMachine moveStates;

// in the future maybe setup a log for who is accessing this if needed
	public Player GetPlayerRef()
	{
		return this;
	}

    public override void _Ready()
    {
		moveStates.Entity = this;
        moveStates.Init(this);
    }

	public override void _UnhandledInput(InputEvent @event)
    {
        //
    }

	public override void _Process(double delta)
    {
		moveState_ChangeCondition();
        moveStates.Process();
    }
	
    public override void _PhysicsProcess(double delta)
    {
        moveStates.PhysicsProcess();
    }


	// Send signal when a state change condition is met.
	// [Signal]
	// public delegate void MoveState_Change0EventHandler();
	[Signal]
	public delegate void MoveState_Change1EventHandler();
	[Signal]
	public delegate void MoveState_Change2EventHandler();

	private int currentState = 1;

	private void moveState_ChangeCondition()
	{
		// if (Velocity == Vector2.Zero && currentState != 0)
		// {
		// 	EmitSignal(SignalName.MoveState_Change0);
		// 	currentState = 0;
		// }
		if (Input.IsActionJustPressed("moveState_walk") && currentState != 1)
		{
			EmitSignal(SignalName.MoveState_Change1);
		}
		if (Input.IsActionJustPressed("moveState_run") && currentState != 2)
		{
			EmitSignal(SignalName.MoveState_Change2);
		}
	}
}
