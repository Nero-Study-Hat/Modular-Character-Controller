using Godot;
using System;
using System.Collections.Generic;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class BodyStateMachine : Node
{
    [Export]
    BaseBodyState startState;

    private CharacterBody2D entity;
    private BaseMoveData[] statesResources;

    [Export]
    BaseBodyStateManager conditionsCheck;

    public BaseBodyState CurrentState {get; private set;}
    public BodyStateFactory.AllBodyStates EnumVal_CurrentState;

    public BodyStateMachine GetBody_StateMachine()
    {
        return this;
    }

    public Dictionary<BodyStateFactory.AllBodyStates, BaseBodyState> entityBodyStatesDict = new Dictionary<BodyStateFactory.AllBodyStates, BaseBodyState>();


    [Signal]
    public delegate void MoveState_ChangedEventHandler(BaseBodyState MoveState);

    public void ChangeState(BaseBodyState newState)
    {
        CurrentState.Exit(entity);
        newState.Enter(entity, statesResources);

        CurrentState = newState;
        var StringVal_CurrentState = CurrentState.GetType().ToString();
        EnumVal_CurrentState = (BodyStateFactory.AllBodyStates)Enum.Parse(typeof(BodyStateFactory.AllBodyStates), StringVal_CurrentState);
        EmitSignal(SignalName.MoveState_Changed, newState);
    }


    public void Init(BaseMoveData[] StatesResources)
    {
        entity = this.GetParent<CharacterBody2D>();
        GetStates();

        statesResources = StatesResources;

        startState.Enter(entity, statesResources);
        CurrentState = startState;

        var StringVal_CurrentState = CurrentState.GetType().ToString();
        EnumVal_CurrentState = (BodyStateFactory.AllBodyStates)Enum.Parse(typeof(BodyStateFactory.AllBodyStates), StringVal_CurrentState);

        conditionsCheck.Initialize(entity, this);
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
            var stateNode = this.GetChild<BaseBodyState>(state);
            var stateName = stateNode.GetType().ToString();
            BodyStateFactory.AllBodyStates stateEnumVal = (BodyStateFactory.AllBodyStates)Enum.Parse(typeof(BodyStateFactory.AllBodyStates), stateName);
            entityBodyStatesDict.Add(stateEnumVal, stateNode);
        }
    }
}
