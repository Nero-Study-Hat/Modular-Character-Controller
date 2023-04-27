using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export]
	private BodyStateMachine moveStatesManager = new BodyStateMachine();

    [ExportGroup("Resource Files")]
    [Export]
    private BaseMoveData bNormalData;

// in the future maybe setup a log for who is accessing this if needed
	public Player GetPlayerRef()
	{
		return this;
	}

    public override void _Ready()
    {
        BaseMoveData[] statesResources = new BaseMoveData[] {bNormalData};
        moveStatesManager.Init(statesResources);
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
