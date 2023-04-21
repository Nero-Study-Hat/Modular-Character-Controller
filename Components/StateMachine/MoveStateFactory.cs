using Godot;
using System;
using System.Collections.Generic;

public class MoveStateFactory
{
    Movement_StateMachine stateMachine;
    Dictionary<MoveStates, Base_MoveState> statesDict;

    public MoveStateFactory(Movement_StateMachine MoveStates_StateMachine)
    {
        stateMachine = MoveStates_StateMachine;
        statesDict = stateMachine.entityMoveStatesDict;
        
    }


    public enum MoveStates
    {
        BIdle_MoveState,
        BNormal_MoveState
    }

    //  Look into reflection to better unstand calling
    //  check functions for only states that exists in 
    //  the entity stateDict.

    public void SpawnMoveState(MoveStates moveState, Dictionary<MoveStates, Action> allConditionsDict, Dictionary<MoveStates, Action> currentConditionsDict)
    {
        string DirPathMoveStates = "res://Components/StateMachine/MoveStates_Scenes/";
        string ScenePath = DirPathMoveStates + "/" + moveState.ToString() + ".tscn";
        var SceneInstance = (Base_MoveState)ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance);

        statesDict.Add(moveState, SceneInstance);
        currentConditionsDict.Add(moveState, allConditionsDict[moveState]);
    }
}