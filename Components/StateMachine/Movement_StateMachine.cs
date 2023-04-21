using Godot;
using System;
using System.Collections.Generic;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class Movement_StateMachine : Node
{
    [Export]
    Base_MoveState startState;

    private CharacterBody2D entity;

    [Export]
    BaseConditionsCheck conditionsCheck;

    public Base_MoveState CurrentState {get; private set;}
    public MoveStateFactory.MoveStates EnumVal_CurrentState;

    public Movement_StateMachine GetMovement_StateMachine()
    {
        return this;
    }

    public Dictionary<MoveStateFactory.MoveStates, Base_MoveState> entityMoveStatesDict = new Dictionary<MoveStateFactory.MoveStates, Base_MoveState>();


    [Signal]
    public delegate void MoveState_ChangedEventHandler(Base_MoveState MoveState);

    public void ChangeState(Base_MoveState newState)
    {
        CurrentState.Exit(entity);
        newState.Enter(entity);

        CurrentState = newState;
        var StringVal_CurrentState = CurrentState.GetType().ToString();
        EnumVal_CurrentState = (MoveStateFactory.MoveStates)Enum.Parse(typeof(MoveStateFactory.MoveStates), StringVal_CurrentState);
        EmitSignal(SignalName.MoveState_Changed, newState);
    }


    public void Init()
    {
        startState.Enter(entity);
        CurrentState = startState;

        entity = this.GetParent<CharacterBody2D>();
        GetStates();

        conditionsCheck.Initialize(entity, this);

        var StringVal_CurrentState = CurrentState.GetType().ToString();
        EnumVal_CurrentState = (MoveStateFactory.MoveStates)Enum.Parse(typeof(MoveStateFactory.MoveStates), StringVal_CurrentState);
    }


    public void Process()
    {
        conditionsCheck.ConditionsChecker();
        CurrentState.Process(entity);
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess(entity);
    }


    private void GetStates()
    {
        int numStates = this.GetChildCount();

        for (int state = 0; state < numStates; state++)
        {
            var stateNode = this.GetChild<Base_MoveState>(state);
            var stateName = stateNode.GetType().ToString();
            MoveStateFactory.MoveStates stateEnumVal = (MoveStateFactory.MoveStates)Enum.Parse(typeof(MoveStateFactory.MoveStates), stateName);
            entityMoveStatesDict.Add(stateEnumVal, stateNode);
        }
    }
}
