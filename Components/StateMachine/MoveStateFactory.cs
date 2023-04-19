using Godot;

public class MoveStateFactory
{
    Movement_StateMachine stateMachine;
    Base_MoveState[] moveStates;

    public MoveStateFactory(Movement_StateMachine MoveStates_StateMachine, Base_MoveState[] MoveStates)
    {
        stateMachine = MoveStates_StateMachine;
        moveStates = MoveStates;
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