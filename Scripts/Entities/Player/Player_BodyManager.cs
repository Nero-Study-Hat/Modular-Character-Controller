using Godot;
using System;
using System.Collections.Generic;

public partial class Player_BodyManager : BaseBodyStateManager
{
    CharacterBody2D player = new CharacterBody2D();
    BodyStateMachine bodyStateMachine = new BodyStateMachine();

    private enum states
    {
        BIdle,
        BNormal
    }

    Dictionary<states, BaseBodyState> stateNodesDict = new Dictionary<states, BaseBodyState>();

    Dictionary<states, Action> currentConditionsDict = new Dictionary<states, Action>();
    Dictionary<states, Action> allConditionsDict = new Dictionary<states, Action>();

    private delegate bool conditionCheck();
    Dictionary<states, conditionCheck> enterConditionsDict = new Dictionary<states, conditionCheck>()
    {
        {states.BNormal, () => Input.IsActionJustPressed("switchNormal")}
    };
    Dictionary<states, conditionCheck> spawnConditionsDict = new Dictionary<states, conditionCheck>()
    {
        {states.BNormal, () => Input.IsActionJustPressed("spawnNormal")}
    };

    // Try testing spawning here with the area2d and a signal.

    private void SetAllStateChecks()
    {
        allConditionsDict.Add(states.BIdle, BIdle_CheckConditions);
        allConditionsDict.Add(states.BNormal, BNormal_CheckConditions);
    }

    private void SetInitialStateChecks()
    {
        currentConditionsDict.Add(states.BIdle, BIdle_CheckConditions);
        // currentConditionsDict.Add(states.BNormal, BNormal_CheckConditions);
    }

    public override void Initialize(CharacterBody2D EntityRef, BodyStateMachine BodyStateMachine)
    {
        player = EntityRef;
        bodyStateMachine = BodyStateMachine;
        
        GetStatesNodes();

        SetAllStateChecks();
        SetInitialStateChecks();
    }

    private void GetStatesNodes()
    {
        int numStates = bodyStateMachine.GetChildCount();

        for (int state = 0; state < numStates; state++)
        {
            var stateNode = bodyStateMachine.GetChild<BaseBodyState>(state);
            var stateName = stateNode.GetType().ToString();
            states stateEnumVal = (states)Enum.Parse(typeof(states), stateName);
            stateNodesDict.Add(stateEnumVal, stateNode);
        }
    }


    public override void ConditionsChecker()
    {
        // currentConditionsDict[body_StateMachine.EnumVal_CurrentState].Invoke();
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
        if (stateNodesDict.ContainsKey(states.BNormal) == true) // Check BNormal Switch
        {
            if (enterConditionsDict[states.BNormal] == true)
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
        if (entityStateDict.ContainsKey(states.BIdle) == false) // Check BIdle Spawn
        {
            var statusBIdle = BIdle_SpawnNewCheck();
            if (statusBIdle == true)
            {
                bodyStateFactory.SpawnMoveState(BodyStateFactory.AllBodyStates.BIdle_MoveState, allConditionsDict, currentConditionsDict);
                return;
            }
        }
    }
}
