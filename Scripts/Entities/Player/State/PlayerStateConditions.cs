using Godot;
using System;
using System.Collections.Generic;
using States = PlayerStateManager.States;

// Condition check utilities for the PlayerStateManager.
public class PlayerStateConditions
{
    private readonly CharacterBody2D _player;
    private readonly Dictionary<PlayerStateManager.States, BaseState> _stateNodesDict;

    public PlayerStateConditions (CharacterBody2D player, Dictionary<PlayerStateManager.States, BaseState> stateNodesDict)
    {
        _player = player;
        _stateNodesDict = stateNodesDict;
    }

    public bool BaseSwitchToBIdle()
    {
        bool inputCheck = Input.IsActionJustPressed("switchIdle");
        bool stateExistsCheck = _stateNodesDict.ContainsKey(States.BIdle_BodyState);
        return inputCheck && stateExistsCheck;
    }

    public bool BaseSpawnBIdle()
    {
        bool inputCheck = Input.IsActionJustPressed("spawnIdle");
        bool stateExistsCheck = _stateNodesDict.ContainsKey(States.BIdle_BodyState) == false;
        return inputCheck && stateExistsCheck;
    }

    public bool BaseSwitchToBNormal()
    {
        bool inputCheck = Input.IsActionJustPressed("switchNormal");
        bool stateExistsCheck = _stateNodesDict.ContainsKey(States.BNormal_BodyState);
        return inputCheck && stateExistsCheck;
    }

    public bool BaseSpawnBNormal()
    {
        bool inputCheck = Input.IsActionJustPressed("spawnNormal");
        bool stateExistsCheck = _stateNodesDict.ContainsKey(States.BNormal_BodyState) == false;
        return inputCheck && stateExistsCheck;
    }

    // Other commonly used conditions can be built here.
    // eg. a VelocityCheck can be made here and resused in
    // the above base conditions and the checks from one state to another
    // in the PlayerStateManager
}