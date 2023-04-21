using Godot;
using System;
using System.Collections.Generic;

public partial class Player_MoveConditionsCheck : BaseConditionsCheck
{
    CharacterBody2D player = new CharacterBody2D();
    Movement_StateMachine movement_StateMachine = new Movement_StateMachine();

    MoveStateFactory.MoveStates enumVal_CurrentState;
    Dictionary<MoveStateFactory.MoveStates, Base_MoveState> entityStateDict = new Dictionary<MoveStateFactory.MoveStates, Base_MoveState>();
    Dictionary<MoveStateFactory.MoveStates, Action> currentConditionsDict = new Dictionary<MoveStateFactory.MoveStates, Action>();


    Dictionary<MoveStateFactory.MoveStates, Action> allConditionsDict = new Dictionary<MoveStateFactory.MoveStates, Action>();

    // Dictionary<MoveStateFactory.MoveStates, Action> conditionChecksDict = new Dictionary<MoveStateFactory.MoveStates, Action>();

    private void SetAllStateChecks()
    {
        allConditionsDict.Add(MoveStateFactory.MoveStates.BIdle_MoveState, BIdle_Check);
        allConditionsDict.Add(MoveStateFactory.MoveStates.BNormal_MoveState, BNormal_Check);
    }

    private void SetInitialStateChecks()
    {
        currentConditionsDict.Add(MoveStateFactory.MoveStates.BIdle_MoveState, BIdle_Check);
        currentConditionsDict.Add(MoveStateFactory.MoveStates.BNormal_MoveState, BNormal_Check);
    }

    public override void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager)
    {
        player = EntityRef;
        movement_StateMachine = MoveStatesManager;
        
        entityStateDict = movement_StateMachine.entityMoveStatesDict;
        SetAllStateChecks();
        SetInitialStateChecks();
    }

    public override void ConditionsChecker()
    {
        currentConditionsDict[movement_StateMachine.EnumVal_CurrentState].Invoke();
    }


    public void BIdle_Check()
    {
        if (Input.IsActionJustPressed("moveState_run"))
        {
            movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BNormal_MoveState]);
        }
    }

    public void BNormal_Check()
    {
        if (Input.IsActionJustPressed("moveState_walk"))
        {
            movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BIdle_MoveState]);
        }
    }
}
