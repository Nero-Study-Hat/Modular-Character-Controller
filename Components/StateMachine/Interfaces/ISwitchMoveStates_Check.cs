using Godot;

public interface ISwitchMoveStates_Check
{
    public void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager);

    public void ConditionsChecker();
}