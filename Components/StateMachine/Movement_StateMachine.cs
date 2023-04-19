using Godot;
using System;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class Movement_StateMachine : Node
{
    [Export]
    Base_MoveState startState;

    private CharacterBody2D entity;
    // private PlayerMoveStates_Conditions moveStates_Conditions;
    private ISwitchMoveStates_Check switchCheck;
    private ISpawnMoveStates_Check spawnCheck;

    public Base_MoveState CurrentState {get; private set;}
    public Base_MoveState[] MoveStates {get; private set;}

    public Movement_StateMachine GetMovement_StateMachine()
    {
        return this;
    }

    [Signal]
    public delegate void MoveState_ChangedEventHandler(Base_MoveState MoveState);

    public void ChangeState(Base_MoveState newState)
    {
        CurrentState.Exit(entity);
        newState.Enter(entity);

        CurrentState = newState;
        EmitSignal(SignalName.MoveState_Changed, newState);
    }


    public void Init(Player player)
    {
        entity = player.GetPlayerRef();
        switchCheck = LoadSwitchCheck_Script(entity);
        spawnCheck = LoadSpawnCheck_Script(entity);

        var numStates = this.GetChildCount();
        MoveStates = new Base_MoveState[numStates];

        for (int index = 0; index < numStates; index++)
        {
            if (this.GetChild(index) is Base_MoveState)
            {
                MoveStates[index] = this.GetChild<Base_MoveState>(index);
            }
        }

        startState.Enter(entity);
        CurrentState = startState;

        switchCheck.Initialize(player, this);
        spawnCheck.Initialize(player, this);
    }


    public void Process()
    {
        switchCheck.ConditionsChecker(MoveStates);
        CurrentState.Process(entity);
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess(entity);
    }


    // All this should be moved to the factory script and should apply to a specific state rather than the state machine.
    public ISwitchMoveStates_Check LoadSwitchCheck_Script(Node entityRef)
    {
        var fileName = this.GetType().ToString;
        var entityFileName = entityRef.GetType().ToString;
        string path = "res://Scripts/Entities/" + entityFileName + "/MoveState_ConditionsFiles/" + "Switch" + "/" + fileName;
        ISwitchMoveStates_Check SwitchCheck = GD.Load<ISwitchMoveStates_Check>(path);

        return SwitchCheck;
    }

        public ISpawnMoveStates_Check LoadSpawnCheck_Script(Node entityRef)
    {
        var fileName = this.GetType().ToString;
        var entityFileName = entityRef.GetType().ToString;
        string path = "res://Scripts/Entities/" + entityFileName + "/MoveState_ConditionsFiles/" + "Switch" + "/" + fileName;
        ISpawnMoveStates_Check SpawnCheck = GD.Load<ISpawnMoveStates_Check>(path);

        return SpawnCheck;
    }
}
