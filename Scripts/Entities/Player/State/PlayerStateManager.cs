using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStateManager : BaseStateManager
{
    [Export]
    ResourcePreloader _customResPreloader;
    [Export]
    ResourcePreloader _generalResPreloader;

    CharacterBody2D _player = new CharacterBody2D();
    StateMachine _stateMachine = new StateMachine();
    PlayerStateConditions _checkUtils;

    [ExportGroup("Body Data")]
    [Export]
    private BNormal_Data[] bNormalData;

    /// <summary>
    /// <para>names must be same as class names</para>
    /// <para>all possible states for this entity must be here</para>
    /// </summary>
    public enum States
    {
        BIdle_BodyState,
        BNormal_BodyState,
    }

    // Must be start state.
    //FIXME: Make this get the export selected start state instead of being hard coded.
    public States _currentState = States.BIdle_BodyState;
    
    Dictionary<States, BaseState> _stateNodesDict = new Dictionary<States, BaseState>();

    private void SetCurrentState(States state)
    {
        _currentState = state;
        //TODO: Move ChangeState method functionality from StateMachine to here
        // it is used a bunch here but never in the state machine class where it lives
        _stateMachine.ChangeState(_stateNodesDict[state]);
    }

    private void SpawnState(States state, ResourcePreloader resourcePreloader)
    {
        var stateName = state.ToString();
        var stateScene = resourcePreloader.GetResource(stateName) as PackedScene; // this line is breaking
        var SceneInstance = (BaseState)stateScene.Instantiate();
        _stateMachine.AddChild(SceneInstance);

        if (!_stateNodesDict.ContainsKey(state))
        {
            _stateNodesDict.Add(state, SceneInstance);
        }
    }

    // Try testing spawning here with the area2d and a signal.

    public override void Initialize(StateMachine StateMachine)
    {
        _player = this.GetOwner<CharacterBody2D>();
        _checkUtils = new PlayerStateConditions(_player, _stateNodesDict);
        _stateMachine = StateMachine;
        
        GetStatesNodes();
    }

    private void GetStatesNodes()
    {
        int numStates = _stateMachine.GetChildCount();

        for (int state = 0; state < numStates; state++)
        {
            var stateNode = _stateMachine.GetChild<BaseState>(state);
            var stateName = stateNode.GetType().ToString();
            States stateEnumVal = (States)Enum.Parse(typeof(States), stateName);
            _stateNodesDict.Add(stateEnumVal, stateNode);
        }
    }

    // Check _currentState switch to other existing states.
    // Check spawning other states while in _currentState.
    // There are specific condition checkers for each state so that
    // specific checks can be run, specific effects can be generated
    // and/or state data changing logic handled based on the current state.
    public override void ConditionsChecker()
    {
        switch (_currentState)
        {
            case States.BIdle_BodyState:
                FromBIdle_CheckSwitch();
                FromBIdle_CheckSpawn();
                break;
            case States.BNormal_BodyState:
                FromBNormal_CheckSwitch();
                FromBNormal_CheckSpawn();
                break;
        }
    }

// -- From BIdle Checking

    private void FromBIdle_CheckSwitch()
    {
        if (_checkUtils.BaseSwitchToBNormal())
        {
            _stateNodesDict[States.BNormal_BodyState].SetData(bNormalData[0]);
            SetCurrentState(States.BNormal_BodyState);
        }
    }

    private void FromBIdle_CheckSpawn()
    {
        if (_checkUtils.BaseSpawnBNormal())
        {
            SpawnState(States.BNormal_BodyState, _customResPreloader);
        }
    }

// -- From BNormal Checking

    private void FromBNormal_CheckSwitch()
    {
        if (_checkUtils.BaseSwitchToBIdle())
        {
            SetCurrentState(States.BIdle_BodyState);
        }
    }

    private void FromBNormal_CheckSpawn()
    {
        if (_checkUtils.BaseSpawnBIdle())
        {
            SpawnState(States.BIdle_BodyState, _customResPreloader);
        }
    }
}
