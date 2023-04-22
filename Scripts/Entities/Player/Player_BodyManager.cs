using Godot;
using System;
using System.Collections.Generic;

public partial class Player_BodyManager : BaseBodyStateManager
{
    CharacterBody2D player = new CharacterBody2D();
    BodyStateMachine body_StateMachine = new BodyStateMachine();

    BodyStateFactory bodyStateFactory;

    BodyStateFactory.AllBodyStates enumVal_CurrentState;
    Dictionary<BodyStateFactory.AllBodyStates, BaseBodyState> entityStateDict = new Dictionary<BodyStateFactory.AllBodyStates, BaseBodyState>();
    Dictionary<BodyStateFactory.AllBodyStates, Action> currentConditionsDict = new Dictionary<BodyStateFactory.AllBodyStates, Action>();


    Dictionary<BodyStateFactory.AllBodyStates, Action> allConditionsDict = new Dictionary<BodyStateFactory.AllBodyStates, Action>();

    // Try testing spawning here with the area2d and a signal.

    private void SetAllStateChecks()
    {
        allConditionsDict.Add(BodyStateFactory.AllBodyStates.BIdle_MoveState, BIdle_CheckConditions);
        allConditionsDict.Add(BodyStateFactory.AllBodyStates.BNormal_MoveState, BNormal_CheckConditions);
    }

    private void SetInitialStateChecks()
    {
        currentConditionsDict.Add(BodyStateFactory.AllBodyStates.BIdle_MoveState, BIdle_CheckConditions);
        // currentConditionsDict.Add(MoveStateFactory.MoveStates.BNormal_MoveState, BNormal_CheckConditions);
    }

    public override void Initialize(CharacterBody2D EntityRef, BodyStateMachine bodyStateMachine)
    {
        player = EntityRef;
        body_StateMachine = bodyStateMachine;

        bodyStateFactory = new BodyStateFactory(body_StateMachine);
        
        entityStateDict = body_StateMachine.entityBodyStatesDict;
        SetAllStateChecks();
        SetInitialStateChecks();
    }

    public override void ConditionsChecker()
    {
        currentConditionsDict[body_StateMachine.EnumVal_CurrentState].Invoke();
    }

// --

    private void BIdle_CheckConditions()
    {
        BIdle_CheckSwitch();
        BIdle_CheckSpawn();
    }

    private void BNormal_CheckConditions()
    {
        BNormal_CheckSwitch();
        BNormal_CheckSpawn();
    }

// --

    private void BIdle_CheckSwitch()
    {
        if (entityStateDict.ContainsKey(BodyStateFactory.AllBodyStates.BNormal_MoveState) == true) // Check BNormal Switch
        {
            var _BNormal_EnterCheck = BNormal_EnterCheck();
            if (_BNormal_EnterCheck == true)
            {
                body_StateMachine.ChangeState(entityStateDict[BodyStateFactory.AllBodyStates.BNormal_MoveState]);
                return;
            }
        }
    }

    private void BIdle_CheckSpawn()
    {
        if (entityStateDict.ContainsKey(BodyStateFactory.AllBodyStates.BNormal_MoveState) == false) // Check BNormal Spawn (Currently empty)
        {
            var statusBNormal = BNormal_SpawnNewCheck();
            if (statusBNormal == true)
            {
                bodyStateFactory.SpawnMoveState(BodyStateFactory.AllBodyStates.BNormal_MoveState, allConditionsDict, currentConditionsDict);
                return;
            }
        }
    }

// --

    private void BNormal_CheckSwitch()
    {
        if (entityStateDict.ContainsKey(BodyStateFactory.AllBodyStates.BIdle_MoveState) == true) // Check BIdle Switch
        {
            var _BIdle_EnterCheck = BIdle_EnterCheck();
            if (_BIdle_EnterCheck == true)
            {
                body_StateMachine.ChangeState(entityStateDict[BodyStateFactory.AllBodyStates.BIdle_MoveState]);
                return;
            }
        }
    }

    private void BNormal_CheckSpawn()
    {
        if (entityStateDict.ContainsKey(BodyStateFactory.AllBodyStates.BIdle_MoveState) == false) // Check BIdle Spawn
        {
            var statusBIdle = BIdle_SpawnNewCheck();
            if (statusBIdle == true)
            {
                bodyStateFactory.SpawnMoveState(BodyStateFactory.AllBodyStates.BIdle_MoveState, allConditionsDict, currentConditionsDict);
                return;
            }
        }
    }

// --


    private bool BIdle_EnterCheck()
    {
        if (Input.IsActionJustPressed("switchIdle"))
        {
            return true;
        }
        return false;
    }

    private bool BIdle_SpawnNewCheck()
    {
        if (Input.IsActionJustPressed("spawnIdle"))
        {
            return true;
        }
        return false;
    }

// --

    private bool BNormal_EnterCheck()
    {
        if (Input.IsActionJustPressed("switchNormal"))
        {
            return true;
        }
        return false;
    }

    private bool BNormal_SpawnNewCheck()
    {
        if (Input.IsActionJustPressed("spawnNormal"))
        {
            return true;
        }
        return false;
    }
}
