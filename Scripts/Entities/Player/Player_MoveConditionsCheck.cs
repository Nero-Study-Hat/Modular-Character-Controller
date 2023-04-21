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

    // Try testing spawning here with the area2d and a signal.

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


    private void BIdle_Check()
    {
        var _BNormal_EnterCheck = BIdle_EnterCheck();
        if (_BNormal_EnterCheck == true)
        {
            movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BNormal_MoveState]);
            return;
        }
    }

    private void BNormal_Check()
    {
        var _BIdle_EnterCheck = BIdle_EnterCheck();
        if (_BIdle_EnterCheck == true)
        {
            movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BIdle_MoveState]);
            return;
        }
    }

    private bool BIdle_EnterCheck()
    {
        if (Input.IsActionJustPressed("moveState_walk"))
        {
            return true;
        }
        return false;
    }

    private bool BNormal_EnterCheck()
    {
        if (Input.IsActionJustPressed("moveState_run"))
        {
            return true;
        }
        return false;
    }
}
