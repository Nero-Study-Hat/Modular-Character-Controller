using Godot;
using System;
using System.Collections.Generic;

public partial class Player_BodyManager : BaseBodyStateManager
{
    [Export]
    ResourcePreloader resourcePreloader;

    CharacterBody2D player = new CharacterBody2D();
    BodyStateMachine bodyStateMachine = new BodyStateMachine();

    private enum states // names must be same as class names
    {
        BIdle_BodyState,
        BNormal_BodyState
    }
    private states currentState = states.BNormal_BodyState; // Must be start state.
    
    Dictionary<states, BaseBodyState> stateNodesDict = new Dictionary<states, BaseBodyState>();

    static string _inputEnterBIdle = "switchIdle";
    static string _inputEnterBNormal = "switchNormal";
    static string _inputSpawnBIdle = "spawnIdle";
    static string _inputSpawnBNormal = "spawnNormal";

    Dictionary<states, Action> allConditionChecks = new Dictionary<states, Action>();
    private void SetAllStateChecks()
    {
        allConditionChecks.Add(states.BIdle_BodyState, BIdle_CheckConditions);
        allConditionChecks.Add(states.BNormal_BodyState, BNormal_CheckConditions);
    }
    Dictionary<states, Func<bool>> allEnterConditions = new Dictionary<states, Func<bool>>()
    {
        {states.BIdle_BodyState, () => Input.IsActionJustPressed(_inputEnterBIdle)},
        {states.BNormal_BodyState, () => Input.IsActionJustPressed(_inputEnterBNormal)}
    };
    Dictionary<states, Func<bool>> allSpawnConditions = new Dictionary<states, Func<bool>>()
    {
        {states.BIdle_BodyState, () => Input.IsActionJustPressed("spawnIdle")},
        {states.BNormal_BodyState, () => Input.IsActionJustPressed("spawnNormal")}
    };

    Dictionary<states, Action> currentConditionChecks = new Dictionary<states, Action>();
    private void SetInitialStateChecks()
    {
        currentConditionChecks.Add(states.BNormal_BodyState, BNormal_CheckConditions);
    }
    Dictionary<states, Func<bool>> currentEnterConditions = new Dictionary<states, Func<bool>>()
    {
        {states.BNormal_BodyState, () => Input.IsActionJustPressed(_inputSpawnBNormal)}
    };
    Dictionary<states, Func<bool>> currentSpawnConditions = new Dictionary<states, Func<bool>>()
    {
        {states.BNormal_BodyState, () => Input.IsActionJustPressed(_inputSpawnBNormal)}
    };

    // Try testing spawning here with the area2d and a signal.

    public override void Initialize(CharacterBody2D EntityRef, BodyStateMachine BodyStateMachine)
    {
        player = EntityRef;
        bodyStateMachine = BodyStateMachine;
        
        SetInitialStateChecks();
        SetAllStateChecks();

        GetStatesNodes();
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
        currentConditionChecks[currentState].Invoke();
    }

// -- // TODO Set up spawning.

    private void BIdle_CheckConditions()
    {
        BIdle_CheckSwitch();
        // BIdle_CheckSpawn();
    }

    private void BNormal_CheckConditions()
    {
        BNormal_CheckSwitch();
        // BNormal_CheckSpawn();
    }

// --

    private void BIdle_CheckSwitch()
    {
        if (stateNodesDict.ContainsKey(states.BNormal_BodyState) == true) // Check BNormal Switch
        {
            if (currentEnterConditions[states.BNormal_BodyState].Invoke() == true)
            {
                bodyStateMachine.ChangeState(stateNodesDict[states.BNormal_BodyState]);
                return;
            }
        }
    }

    // private void BIdle_CheckSpawn()
    // {
    //     if (stateNodesDict.ContainsKey(states.BNormal) == false) // Check BNormal Spawn (Currently empty)
    //     {
    //         if (spawnConditionsDict[states.BNormal].Invoke() == true)
    //         {
    //             bodyStateMachine.SpawnMoveState(BodyStateFactory.AllBodyStates.BNormal_MoveState, allConditionsDict, currentConditionsDict);
    //             return;
    //         }
    //     }
    // }

// --

    private void BNormal_CheckSwitch()
    {
        if (stateNodesDict.ContainsKey(states.BIdle_BodyState) == true) // Check BIdle Switch
        {
            if (currentEnterConditions[states.BIdle_BodyState].Invoke() == true)
            {
                bodyStateMachine.ChangeState(stateNodesDict[states.BIdle_BodyState]);
                return;
            }
        }
    }

    // private void BNormal_CheckSpawn()
    // {
    //     if (stateNodesDict.ContainsKey(states.BIdle) == false) // Check BIdle Spawn
    //     {
    //         if (spawnConditionsDict[states.BIdle].Invoke() == true)
    //         {
    //             bodyStateFactory.SpawnMoveState(BodyStateFactory.AllBodyStates.BIdle_MoveState, allConditionsDict, currentConditionsDict);
    //             return;
    //         }
    //     }
    // }

    private void SpawnState(states state)
    {
        var stateName = state.ToString();
        var stateScene = resourcePreloader.GetResource(nameof(stateName)) as PackedScene; // Idea, get all at init like conditions
        var SceneInstance = (BaseBodyState)stateScene.Instantiate();
        bodyStateMachine.AddChild(SceneInstance);

        stateNodesDict.Add(state, SceneInstance);
        currentConditionChecks.Add(state, allConditionChecks[state]);
        currentEnterConditions.Add(state, allEnterConditions[state]);
        currentSpawnConditions.Add(state, allSpawnConditions[state]);
    }
}
