using Godot;

public class BNormal_SwitchCheck:ISwitchMoveStates_Check
{
    CharacterBody2D player;
    Movement_StateMachine movement_StateMachine;

    public void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager)
    {
        player = EntityRef;
        movement_StateMachine = MoveStatesManager;
    }

    public void ConditionsChecker(Base_MoveState[] moveStates) // call in process
    {
        if (CheckState1() == true)
        {
            movement_StateMachine.ChangeState(moveStates[0]);
        }
    }
    

    public bool CheckState1()
    {
        if (Input.IsActionJustPressed("moveState_walk"))
        {
            return true;
        }
        
        return false;
    }
}