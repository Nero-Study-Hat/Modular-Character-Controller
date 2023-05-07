using Godot;
using System;
using System.Collections.Generic;

public abstract partial class BaseStateManager : Node
{
    public abstract void Initialize(StateMachine stateMachine);

    public abstract void ConditionsChecker();
}