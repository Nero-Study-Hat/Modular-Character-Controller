using Godot;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public abstract partial class Base_MoveState : Node
{
    // [Export]
    // protected PhysicsBody2D entity;

    public abstract void Enter(CharacterBody2D entity);

    public abstract void Exit(CharacterBody2D entity);

    public abstract void Process(CharacterBody2D entity);

    public abstract void PhysicsProcess(CharacterBody2D entity);


    public void InitializeCheckDependencies(CharacterBody2D entity, Base_MoveState state, ISwitchMoveStates_Check switchCheck, ISpawnMoveStates_Check spawnCheck)
    {
        switchCheck = LoadSwitchCheck_Script(entity, state);
        spawnCheck = LoadSpawnCheck_Script(entity, state);
    }

    public ISwitchMoveStates_Check LoadSwitchCheck_Script(Node entityRef, Base_MoveState currentState)
    {
        var fileName = currentState.GetType().ToString;
        var entityFileName = entityRef.GetType().ToString;
        string path = "res://Scripts/Entities/" + entityFileName + "/MoveState_ConditionsFiles/" + "Switch" + "/" + fileName + ".cs";
        ISwitchMoveStates_Check SwitchCheck = GD.Load<ISwitchMoveStates_Check>(path);

        return SwitchCheck;
    }

    public ISpawnMoveStates_Check LoadSpawnCheck_Script(Node entityRef, Base_MoveState currentState)
    {
        var fileName = currentState.GetType().ToString;
        var entityFileName = entityRef.GetType().ToString;
        string path = "res://Scripts/Entities/" + entityFileName + "/MoveState_ConditionsFiles/" + "Spawn" + "/" + fileName + ".cs";
        ISpawnMoveStates_Check SpawnCheck = GD.Load<ISpawnMoveStates_Check>(path);

        return SpawnCheck;
    }
}