using Godot;
using System;
using System.Collections.Generic;

public abstract partial class BaseConditionsCheck : Node
{
    public abstract void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager);

    public abstract void ConditionsChecker();
}