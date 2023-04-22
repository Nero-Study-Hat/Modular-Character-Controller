using Godot;
using System;
using System.Collections.Generic;

public class BodyStateFactory
{
    BodyStateMachine stateMachine;
    Dictionary<BodyStates, BaseBodyState> statesDict;

    public BodyStateFactory(BodyStateMachine BodyStateMachine)
    {
        stateMachine = BodyStateMachine;
        statesDict = stateMachine.entityBodyStatesDict;
        
    }
    

    public enum AllBodyStates
    {
        BIdle_MoveState,
        BNormal_MoveState
    }

    public enum PlayerStateGroups
    {
        //
    }

    //  Look into reflection to better unstand calling
    //  check functions for only states that exists in 
    //  the entity stateDict.

    public void SpawnMoveState(BodyStates bodyState, Dictionary<BodyStates, Action> allConditionsDict, Dictionary<BodyStates, Action> currentConditionsDict)
    {
        string DirPathMoveStates = "res://Components/StateMachine/MoveStates_Scenes/";
        string ScenePath = DirPathMoveStates + "/" + bodyState.ToString() + ".tscn";
        var SceneInstance = (BaseBodyState)ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance);

        statesDict.Add(bodyState, SceneInstance);
        currentConditionsDict.Add(bodyState, allConditionsDict[bodyState]);
    }

    public void AddPlayerMoveState(BodyStates bodyState, Dictionary<BodyStates, Action> allConditionsDict, Dictionary<BodyStates, Action> currentConditionsDict)
    {
        string DirPathMoveStates = "res://Components/StateMachine/MoveStates_Scenes/";
        string ScenePath = DirPathMoveStates + "/" + bodyState.ToString() + ".tscn";
        var SceneInstance = (BaseBodyState)ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance);

        statesDict.Add(bodyState, SceneInstance);
        currentConditionsDict.Add(bodyState, allConditionsDict[bodyState]);
    }

    public void AddCustomMoveState(string customScenePath)
    {
        //
    }
}