using Godot;
using System;
using System.Collections.Generic;

public class BodyStateFactory
{
    BodyStateMachine stateMachine;
    Dictionary<AllBodyStates, BaseBodyState> entityStatesDict;

    Dictionary<BodyStateTypes, string> stateTypesDict;
    string generalStatesDirPath = "res://Components/StateMachine/MoveStates_Scenes/General/";

    Dictionary<BodyStateGroups, AllBodyStates[]> stateGroupsDict;

    public BodyStateFactory(BodyStateMachine BodyStateMachine)
    {
        stateMachine = BodyStateMachine;
        entityStatesDict = stateMachine.entityBodyStatesDict;

        stateTypesDict = new Dictionary<BodyStateTypes, string>
        {
            {BodyStateTypes.Player, "PlayerStates/"},
            {BodyStateTypes.AI, "AIStates/"}
        };

        stateGroupsDict = new Dictionary<BodyStateGroups, AllBodyStates[]>
        {
            {BodyStateGroups.TestGroup, new AllBodyStates[] {AllBodyStates.BIdle_MoveState, AllBodyStates.BNormal_MoveState}}
        };
        
    }
    

    public enum BodyStateTypes
    {
        Player,
        AI
    }

    public enum BodyStateGroups
    {
        TestGroup
    }

    public enum AllBodyStates
    {
        BIdle_MoveState,
        BNormal_MoveState
    }

    //  Look into reflection to better unstand calling
    //  check functions for only states that exists in 
    //  the entity stateDict.

    public void SpawnMoveState(AllBodyStates bodyState, Dictionary<AllBodyStates, Action> allConditionsDict, Dictionary<AllBodyStates, Action> currentConditionsDict)
    {
        string DirPathBodyStates = "res://Components/StateMachine/MoveStates_Scenes/";
        string ScenePath = DirPathBodyStates + "/" + bodyState.ToString() + ".tscn";
        var SceneInstance = (BaseBodyState)ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance);

        entityStatesDict.Add(bodyState, SceneInstance);
        currentConditionsDict.Add(bodyState, allConditionsDict[bodyState]);
    }

    public void AddGeneralState(BodyStateTypes bodyStateType, AllBodyStates bodyState, Dictionary<AllBodyStates, Action> allConditionsDict, Dictionary<AllBodyStates, Action> currentConditionsDict)
    {
        string DirPathBodyStates = generalStatesDirPath + stateTypesDict[bodyStateType];
        string ScenePath = DirPathBodyStates + "/" + bodyState.ToString() + ".tscn";
        var SceneInstance = (BaseBodyState)ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance);

        entityStatesDict.Add(bodyState, SceneInstance);
        currentConditionsDict.Add(bodyState, allConditionsDict[bodyState]);
    }

    // Thoughts
    // - needs more than single dimension array holding state enum values
    //  - needs state type for each index
    public void AddGeneralStateGroup(BodyStateTypes bodyStateType, BodyStateGroups bodyStateGroup, Dictionary<AllBodyStates, Action> allConditionsDict, Dictionary<AllBodyStates, Action> currentConditionsDict)
    {
        string DirPathBodyStates = generalStatesDirPath + stateTypesDict[bodyStateType];

        foreach (AllBodyStates state in stateGroupsDict[bodyStateGroup])
        {
            string ScenePath = DirPathBodyStates + "/" + state.ToString() + ".tscn";
            var SceneInstance = (BaseBodyState)ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
            stateMachine.AddChild(SceneInstance);

            entityStatesDict.Add(state, SceneInstance);
            currentConditionsDict.Add(state, allConditionsDict[state]);
        }
    }


    public void AddCustomState(string customScenePath)
    {
        // string ScenePath = entityDirPath + "/CustomBodyStates/" + bodyStateType(can be empty) + bodyState(passed in as enumVal.ToString()) + ".tscn";
    }

    public void AddCustomStateGroup(BodyStateTypes bodyStateType, AllBodyStates[] bodyStates, Dictionary<AllBodyStates, Action> allConditionsDict, Dictionary<AllBodyStates, Action> currentConditionsDict)
    {
        string DirPathBodyStates = generalStatesDirPath + stateTypesDict[bodyStateType];

        foreach (AllBodyStates state in bodyStates)
        {
            string ScenePath = DirPathBodyStates + "/" + state.ToString() + ".tscn";
            var SceneInstance = (BaseBodyState)ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
            stateMachine.AddChild(SceneInstance);

            entityStatesDict.Add(state, SceneInstance);
            currentConditionsDict.Add(state, allConditionsDict[state]);
        }
    }
}