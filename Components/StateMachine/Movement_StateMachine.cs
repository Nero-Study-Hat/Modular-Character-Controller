using Godot;
using System;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class Movement_StateMachine : Node
{
    [Export]
    Base_MoveState startState;

    private CharacterBody2D entity;
    private PlayerMoveStates_Conditions moveStates_Conditions;

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

        moveStates_Conditions = new PlayerMoveStates_Conditions(entity, this);
    }


    public void Process()
    {
        moveStates_Conditions.ConditionsChecker(CurrentState, MoveStates);
        CurrentState.Process(entity);
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess(entity);
    }


    public void LoadMoveState(Base_MoveState moveState)
    {
        string ScenePath = "res://Components/StateMachine/MoveStates_Scenes/" + moveState + ".tscn";
        var SceneInstance = ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        AddChild(SceneInstance); // not sure yet here
    }
}