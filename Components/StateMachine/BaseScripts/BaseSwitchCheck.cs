using Godot;

public abstract partial class BaseSwitchCheck : Node
{
    public abstract void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager);

    public abstract void ConditionsChecker();
}