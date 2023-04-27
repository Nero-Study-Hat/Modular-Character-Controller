using Godot;
using System;
using System.Collections.Generic;

public partial class Player_BodyManager : BaseBodyStateManager
{
    [Export]
    ResourcePreloader _customResPreloader;
    [Export]
    ResourcePreloader _generalResPreloader;
    CharacterBody2D _player = new CharacterBody2D();
    BodyStateMachine _bodyStateMachine = new BodyStateMachine();

    public enum states // names must be same as class names
    {
        BIdle_BodyState,
        BNormal_BodyState
    }
    public states _currentState = states.BNormal_BodyState; // Must be start state.
    
    Dictionary<states, BaseBodyState> _stateNodesDict = new Dictionary<states, BaseBodyState>();

    Dictionary<states, Func<bool>> _enterConditions = new Dictionary<states, Func<bool>>();
    Dictionary<states, Func<bool>> _spawnConditions = new Dictionary<states, Func<bool>>();

    private void SetConditionChecks()
    {
        _enterConditions.Add(states.BIdle_BodyState, () => Input.IsActionJustPressed("switchIdle"));
        _enterConditions.Add(states.BNormal_BodyState, () => Input.IsActionJustPressed("switchNormal"));

        _spawnConditions.Add(states.BIdle_BodyState, () => Input.IsActionJustPressed("spawnIdle"));
        _spawnConditions.Add(states.BNormal_BodyState, () => Input.IsActionJustPressed("spawnNormal"));
    }

    private void SetCurrentState(states state)
    {
        _currentState = state;
        _bodyStateMachine.ChangeState(_stateNodesDict[state]);
    }

    private void SpawnState(states state, ResourcePreloader resourcePreloader)
    {
        var stateName = state.ToString();
        var stateScene = resourcePreloader.GetResource(stateName) as PackedScene; // this line is breaking
        var SceneInstance = (BaseBodyState)stateScene.Instantiate();
        _bodyStateMachine.AddChild(SceneInstance);

        _stateNodesDict.Add(state, SceneInstance);
    }

    // Try testing spawning here with the area2d and a signal.

    public override void Initialize(CharacterBody2D EntityRef, BodyStateMachine BodyStateMachine)
    {
        _player = EntityRef;
        _bodyStateMachine = BodyStateMachine;
        
        SetConditionChecks();
        GetStatesNodes();
    }

    private void GetStatesNodes()
    {
        int numStates = _bodyStateMachine.GetChildCount();

        for (int state = 0; state < numStates; state++)
        {
            var stateNode = _bodyStateMachine.GetChild<BaseBodyState>(state);
            var stateName = stateNode.GetType().ToString();
            states stateEnumVal = (states)Enum.Parse(typeof(states), stateName);
            _stateNodesDict.Add(stateEnumVal, stateNode);
        }
    }


    public override void ConditionsChecker()
    {
        switch (_currentState)
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
        if (_stateNodesDict.ContainsKey(states.BNormal_BodyState) == true) // Check BNormal Switch
        {
            if (_enterConditions[states.BNormal_BodyState].Invoke() == true)
            {
                SetCurrentState(states.BNormal_BodyState);
                return;
            }
        }
    }

    private void BIdle_CheckSpawn()
    {
        if (_stateNodesDict.ContainsKey(states.BNormal_BodyState) == false) // Check BNormal Spawn (Currently empty)
        {
            if (_spawnConditions[states.BNormal_BodyState].Invoke() == true)
            {
                SpawnState(states.BIdle_BodyState, _customResPreloader);
                return;
            }
        }
    }

// --

    private void BNormal_CheckSwitch()
    {
        if (_stateNodesDict.ContainsKey(states.BIdle_BodyState) == true) // Check BIdle Switch
        {
            if (_enterConditions[states.BIdle_BodyState].Invoke() == true)
            {
                SetCurrentState(states.BIdle_BodyState);
                return;
            }
        }
    }

    private void BNormal_CheckSpawn()
    {
        if (_stateNodesDict.ContainsKey(states.BIdle_BodyState) == false) // Check BIdle Spawn
        {
            if (_spawnConditions[states.BIdle_BodyState].Invoke() == true)
            {
                SpawnState(states.BIdle_BodyState, _customResPreloader);
                return;
            }
        }
    }
}
