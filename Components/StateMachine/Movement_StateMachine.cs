using Godot;
using System;
using System.Collections.Generic;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class Movement_StateMachine : Node
{
    [Export]
    Base_MoveState startState;
    [Export]
    Node ConditionCheckGroup = new Node();

    private CharacterBody2D entity;
    // private PlayerMoveStates_Conditions moveStates_Conditions;

    [Export]
    BaseSwitchCheck switchCheck;

    // private ISwitchMoveStates_Check switchCheck;
    // private ISpawnMoveStates_Check spawnCheck;

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

        // ConnectConditionsScripts(entity);
        // switchCheck = conditionsCheckGroup.GetSwitchScript();
        
        switchCheck.Initialize(entity, this);

        var StringVal_CurrentState = CurrentState.GetType().ToString();
        EnumVal_CurrentState = (MoveStateFactory.MoveStates)Enum.Parse(typeof(MoveStateFactory.MoveStates), StringVal_CurrentState);
    }


    public void Process()
    {
        if (switchCheck != null)
        {
            switchCheck.ConditionsChecker();
        }
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

    // private void ConnectConditionsScripts(CharacterBody2D entity)
    // {
    //     for (int index = 0; index < ConditionCheckGroup.GetChildCount(); index++)
    //     {
    //         var node = ConditionCheckGroup.GetChild(index);

    //         if (node is ISwitchMoveStates_Check)
    //         {
    //             switchCheck = this.GetChild<ISwitchMoveStates_Check>(index);
    //             switchCheck.Initialize(entity, this);
    //         }
    //         if (node is ISpawnMoveStates_Check)
    //         {
    //             spawnCheck = this.GetChild<ISpawnMoveStates_Check>(index);
    //             spawnCheck.Initialize(entity, this);
    //         }
    //     }
    // }
}
