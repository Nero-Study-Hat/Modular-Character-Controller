using Godot;

public interface ISpawnMoveStates_Check
{
    public void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager);

    public void ConditionsChecker(Base_MoveState[] moveStates);
}