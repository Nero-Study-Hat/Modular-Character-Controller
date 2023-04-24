using Godot;
using System;
using System.Collections.Generic;

public partial class Player_BodyManager : BaseBodyStateManager
{
    [Export]
    ResourcePreloader customResPreloader;
    [Export]
    ResourcePreloader generalResPreloader;
    CharacterBody2D player = new CharacterBody2D();
    BodyStateMachine bodyStateMachine = new BodyStateMachine();

    public enum states // names must be same as class names
    {
        BIdle_BodyState,
        BNormal_BodyState
    }
    public states currentState = states.BNormal_BodyState; // Must be start state.
    
    Dictionary<states, BaseBodyState> stateNodesDict = new Dictionary<states, BaseBodyState>();

    Dictionary<states, Func<bool>> enterConditions = new Dictionary<states, Func<bool>>();
    Dictionary<states, Func<bool>> spawnConditions = new Dictionary<states, Func<bool>>();

    private void SetConditionChecks()
    {
        enterConditions.Add(states.BIdle_BodyState, () => Input.IsActionJustPressed("switchIdle"));
        enterConditions.Add(states.BNormal_BodyState, () => Input.IsActionJustPressed("switchNormal"));

        spawnConditions.Add(states.BIdle_BodyState, () => Input.IsActionJustPressed("spawnIdle"));
        spawnConditions.Add(states.BNormal_BodyState, () => Input.IsActionJustPressed("spawnNormal"));
    }

    private void SetCurrentState(states state)
    {
        currentState = state;
        bodyStateMachine.ChangeState(stateNodesDict[state]);
    }

    private void SpawnState(states state, ResourcePreloader resourcePreloader)
    {
        var stateName = state.ToString();
        var stateScene = resourcePreloader.GetResource(stateName) as PackedScene; // this line is breaking
        var SceneInstance = (BaseBodyState)stateScene.Instantiate();
        bodyStateMachine.AddChild(SceneInstance);

        stateNodesDict.Add(state, SceneInstance);
    }

    // Try testing spawning here with the area2d and a signal.

    public override void Initialize(CharacterBody2D EntityRef, BodyStateMachine BodyStateMachine)
    {
        player = EntityRef;
        bodyStateMachine = BodyStateMachine;
        
        SetConditionChecks();
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
        switch (currentState)
        {
            case states.BIdle_BodyState:
                BIdle_CheckConditions();
                break;
            case states.BNormal_BodyState:
                BNormal_CheckConditions();
                break;
        }
    }

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
        if (stateNodesDict.ContainsKey(states.BNormal_BodyState) == true) // Check BNormal Switch
        {
            if (enterConditions[states.BNormal_BodyState].Invoke() == true)
            {
                SetCurrentState(states.BNormal_BodyState);
                return;
            }
        }
    }

    private void BIdle_CheckSpawn()
    {
        if (stateNodesDict.ContainsKey(states.BNormal_BodyState) == false) // Check BNormal Spawn (Currently empty)
        {
            if (spawnConditions[states.BNormal_BodyState].Invoke() == true)
            {
                SpawnState(states.BIdle_BodyState, customResPreloader);
                return;
            }
        }
    }

// --

    private void BNormal_CheckSwitch()
    {
        if (stateNodesDict.ContainsKey(states.BIdle_BodyState) == true) // Check BIdle Switch
        {
            if (enterConditions[states.BIdle_BodyState].Invoke() == true)
            {
                SetCurrentState(states.BIdle_BodyState);
                return;
            }
        }
    }

    private void BNormal_CheckSpawn()
    {
        if (stateNodesDict.ContainsKey(states.BIdle_BodyState) == false) // Check BIdle Spawn
        {
            if (spawnConditions[states.BIdle_BodyState].Invoke() == true)
            {
                SpawnState(states.BIdle_BodyState, customResPreloader);
                return;
            }
        }
    }
}
