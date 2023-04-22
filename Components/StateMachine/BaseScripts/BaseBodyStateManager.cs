using Godot;
using System;
using System.Collections.Generic;

public abstract partial class BaseBodyStateManager : Node
{
    public abstract void Initialize(CharacterBody2D entityRef, BodyStateMachine bodyStatesMachine);

    public abstract void ConditionsChecker();
}