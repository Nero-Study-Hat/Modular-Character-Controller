using Godot;
using System;

public partial class ConditionsCheckGroup : Node
{
    Movement_StateMachine stateMachine;

    ISwitchMoveStates_Check switchCheck;
    ISpawnMoveStates_Check spawnCheck;

    public override void _Ready()
    {
        for (int index = 0; index < this.GetChildCount(); index++)
        {
            if (this.GetChild(index) is ISwitchMoveStates_Check)
            {
                switchCheck = this.GetChild<ISwitchMoveStates_Check>(index);
            }
            if (this.GetChild(index) is ISpawnMoveStates_Check)
            {
                spawnCheck = this.GetChild<ISpawnMoveStates_Check>(index);
            }
        }
    }

    public ISwitchMoveStates_Check GetSwitchScript()
    {
        return switchCheck;
    }

    public ISpawnMoveStates_Check GetSpawnScript()
    {
        return spawnCheck;
    }
}
