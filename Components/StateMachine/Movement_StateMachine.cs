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
        var entityName = entity.GetType().ToString;
        string entityFileName = "" + entityName;

        Node entityScript = new Node();
        string entityFile = entityScript.GetType().ToString();

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

        switchCheck = LoadSwitchCheck_Script(entityFileName);
        spawnCheck = LoadSpawnCheck_Script(entity);

        switchCheck.Initialize(entity, this);
    }


    public void Process()
    {
        CurrentState.Process(entity);
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess(entity);
    }


    public ISwitchMoveStates_Check LoadSwitchCheck_Script(string entityFileName)
    {
        string path = "res://Scripts/Entities/" + entityFileName + "/" + entityFileName + "MoveStates_SwitchCheck" + ".cs";
        ISwitchMoveStates_Check _switchCheck = GD.Load<ISwitchMoveStates_Check>(path);

        return _switchCheck;
    }

    public ISpawnMoveStates_Check LoadSpawnCheck_Script(Node entityRef)
    {
        var entityFileName = entityRef.GetType().ToString;
        string path = "res://Scripts/Entities/" + entityFileName + "/" + entityFileName + "MoveStates_SpawnCheck" + ".cs";
        ISpawnMoveStates_Check _spawnCheck = GD.Load<ISpawnMoveStates_Check>(path);

        return _spawnCheck;
    }
}
