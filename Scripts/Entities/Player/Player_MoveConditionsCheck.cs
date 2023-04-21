using Godot;
using System;
using System.Collections.Generic;

public partial class Player_MoveConditionsCheck : BaseConditionsCheck
{
    CharacterBody2D player = new CharacterBody2D();
    Movement_StateMachine movement_StateMachine = new Movement_StateMachine();

    MoveStateFactory moveStateFactory;

    MoveStateFactory.MoveStates enumVal_CurrentState;
    Dictionary<MoveStateFactory.MoveStates, Base_MoveState> entityStateDict = new Dictionary<MoveStateFactory.MoveStates, Base_MoveState>();
    Dictionary<MoveStateFactory.MoveStates, Action> currentConditionsDict = new Dictionary<MoveStateFactory.MoveStates, Action>();


    Dictionary<MoveStateFactory.MoveStates, Action> allConditionsDict = new Dictionary<MoveStateFactory.MoveStates, Action>();

    // Try testing spawning here with the area2d and a signal.

    private void SetAllStateChecks()
    {
        allConditionsDict.Add(MoveStateFactory.MoveStates.BIdle_MoveState, BIdle_CheckConditions);
        allConditionsDict.Add(MoveStateFactory.MoveStates.BNormal_MoveState, BNormal_CheckConditions);
    }

    private void SetInitialStateChecks()
    {
        currentConditionsDict.Add(MoveStateFactory.MoveStates.BIdle_MoveState, BIdle_CheckConditions);
        // currentConditionsDict.Add(MoveStateFactory.MoveStates.BNormal_MoveState, BNormal_CheckConditions);
    }

    public override void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager)
    {
        player = EntityRef;
        movement_StateMachine = MoveStatesManager;

        moveStateFactory = new MoveStateFactory(movement_StateMachine);
        
        entityStateDict = movement_StateMachine.entityMoveStatesDict;
        SetAllStateChecks();
        SetInitialStateChecks();
    }

    public override void ConditionsChecker()
    {
        currentConditionsDict[movement_StateMachine.EnumVal_CurrentState].Invoke();
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
        if (entityStateDict.ContainsKey(MoveStateFactory.MoveStates.BNormal_MoveState) == true) // Check BNormal Switch
        {
            var _BNormal_EnterCheck = BNormal_EnterCheck();
            if (_BNormal_EnterCheck == true)
            {
                movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BNormal_MoveState]);
                return;
            }
        }
    }

    private void BIdle_CheckSpawn()
    {
        if (entityStateDict.ContainsKey(MoveStateFactory.MoveStates.BNormal_MoveState) == false) // Check BNormal Spawn (Currently empty)
        {
            var statusBNormal = BNormal_SpawnNewCheck();
            if (statusBNormal == true)
            {
                moveStateFactory.SpawnMoveState(MoveStateFactory.MoveStates.BNormal_MoveState, allConditionsDict, currentConditionsDict);
                return;
            }
        }
    }

// --

    private void BNormal_CheckSwitch()
    {
        if (entityStateDict.ContainsKey(MoveStateFactory.MoveStates.BIdle_MoveState) == true) // Check BIdle Switch
        {
            var _BIdle_EnterCheck = BIdle_EnterCheck();
            if (_BIdle_EnterCheck == true)
            {
                movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BIdle_MoveState]);
                return;
            }
        }
    }

    private void BNormal_CheckSpawn()
    {
        if (entityStateDict.ContainsKey(MoveStateFactory.MoveStates.BIdle_MoveState) == false) // Check BIdle Spawn
        {
            var statusBIdle = BIdle_SpawnNewCheck();
            if (statusBIdle == true)
            {
                moveStateFactory.SpawnMoveState(MoveStateFactory.MoveStates.BIdle_MoveState, allConditionsDict, currentConditionsDict);
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
