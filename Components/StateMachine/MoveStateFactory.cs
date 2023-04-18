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

    public void CustomMoveState_Path()
    {
        string ScenePath = "res://Components/StateMachine/MoveStates_Scenes/" + moveStates + ".tscn";
        var SceneInstance = ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance); // not sure yet here
    }

    public void GenerateState()
    {
        string ScenePath = "res://Components/StateMachine/MoveStates_Scenes/" + moveStates + ".tscn";
        var SceneInstance = ResourceLoader.Load<PackedScene>(ScenePath).Instantiate();
        stateMachine.AddChild(SceneInstance); // not sure yet here
    }
}