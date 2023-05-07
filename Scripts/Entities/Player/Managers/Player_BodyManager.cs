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

    [ExportGroup("Body Data")]
    [Export]
    private BaseBodyData[] bNormalData;

    public enum States // names must be same as class names
    {
        BIdle_BodyState,
        BNormal_BodyState,
    }
    public States _currentState = States.BNormal_BodyState; // Must be start state.
    
    Dictionary<States, BaseBodyState> _stateNodesDict = new Dictionary<States, BaseBodyState>();

    Dictionary<States, Func<bool>> _enterConditions = new Dictionary<States, Func<bool>>();
    Dictionary<States, Func<bool>> _spawnConditions = new Dictionary<States, Func<bool>>();

    private void SetConditionChecks()
    {
        _enterConditions.Add(States.BIdle_BodyState, () => Input.IsActionJustPressed("switchIdle"));
        _enterConditions.Add(States.BNormal_BodyState, () => Input.IsActionJustPressed("switchNormal"));

        _spawnConditions.Add(States.BIdle_BodyState, () => Input.IsActionJustPressed("spawnIdle"));
        _spawnConditions.Add(States.BNormal_BodyState, () => Input.IsActionJustPressed("spawnNormal"));
    }

    private void SetCurrentState(States state)
    {
        _currentState = state;
        _bodyStateMachine.ChangeState(_stateNodesDict[state]);
    }

    private void SpawnState(States state, ResourcePreloader resourcePreloader)
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
            States stateEnumVal = (States)Enum.Parse(typeof(States), stateName);
            _stateNodesDict.Add(stateEnumVal, stateNode);
        }
    }


    public override void ConditionsChecker()
    {
        switch (_currentState)
        {
            case States.BIdle_BodyState:
                BIdle_CheckConditions();
                break;
            case States.BNormal_BodyState:
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

// -- BIdle Checking

    private void BIdle_CheckSwitch()
    {
        if (_stateNodesDict.ContainsKey(States.BNormal_BodyState) == true) // Check BNormal Switch
        {
            if (_enterConditions[States.BNormal_BodyState].Invoke() == true) // Check BNormal sub state 0.
            {
                _stateNodesDict[States.BNormal_BodyState].SetResource(bNormalData[0]);
                SetCurrentState(States.BNormal_BodyState);
                return;
            }
        }
    }

    private void BIdle_CheckSpawn()
    {
        if (_stateNodesDict.ContainsKey(States.BNormal_BodyState) == false) // Check BNormal Spawn (Currently empty)
        {
            if (_spawnConditions[States.BNormal_BodyState].Invoke() == true)
            {
                SpawnState(States.BIdle_BodyState, _customResPreloader);
                return;
            }
        }
    }

// -- BNormal Checking

    private void BNormal_CheckSwitch()
    {
        if (_stateNodesDict.ContainsKey(States.BIdle_BodyState) == true) // Check BIdle Switch
        {
            if (_enterConditions[States.BIdle_BodyState].Invoke() == true)
            {
                SetCurrentState(States.BIdle_BodyState);
                return;
            }
        }
    }

    private void BNormal_CheckSpawn()
    {
        if (_stateNodesDict.ContainsKey(States.BIdle_BodyState) == false) // Check BIdle Spawn
        {
            if (_spawnConditions[States.BIdle_BodyState].Invoke() == true)
            {
                SpawnState(States.BIdle_BodyState, _customResPreloader);
                return;
            }
        }
    }
}
