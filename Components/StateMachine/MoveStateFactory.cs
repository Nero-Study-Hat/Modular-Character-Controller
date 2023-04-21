using Godot;
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

    string DirPathMoveStates = "res://Components/StateMachine/MoveStates_Scenes/";


    public void BNormal()
    {
        string Scene = "Normal_BMoveState";
        string ScenePath = DirPathMoveStates + "/" + Scene + ".tscn";
        var SceneInstance = ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance); // not sure yet here
    }
}