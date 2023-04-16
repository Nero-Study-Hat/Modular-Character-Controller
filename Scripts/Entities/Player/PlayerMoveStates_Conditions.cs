using Godot;

public class PlayerMoveStates_Conditions
{
    CharacterBody2D player;
    Movement_StateMachine movement_StateMachine;
    Base_MoveState currentState;
    Base_MoveState[] moveStates;

    public PlayerMoveStates_Conditions(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager, Base_MoveState CurrentState, Base_MoveState[] MoveStates)
    {
        player = EntityRef;
        movement_StateMachine = MoveStatesManager;
        currentState = CurrentState;
        moveStates = MoveStates;
    }

    public void ConditionsChecker() // call in process
    {
        if (currentState == movement_StateMachine.MoveStates[1])
        {
            if (CheckState2() == true)
            {
                movement_StateMachine.ChangeState(moveStates[2]);
            }
            return;
        }

        if (currentState == movement_StateMachine.MoveStates[2])
        {
            if (CheckState1() == true)
            {
                movement_StateMachine.ChangeState(moveStates[1]);
            }
            return;
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

    public bool CheckState2()
    {
        if (Input.IsActionJustPressed("moveState_run"))
        {
            return true;
        }

        return false;
    }
}