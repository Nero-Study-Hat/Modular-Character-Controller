using Godot;

public class PlayerMoveStates_Conditions
{
    Player player;
    Movement_StateMachine moveStatesManager;
    Base_MoveState currentState;

    public PlayerMoveStates_Conditions (Player playerRef, Movement_StateMachine movement_StateMachine, Base_MoveState CurrentState)
    {
        player = playerRef;
        moveStatesManager = movement_StateMachine;
        currentState = CurrentState;
    }


    public void ConditionsChecker() // call in process
    {
        if (currentState == moveStatesManager.MoveStates[1])
        {
            CheckState2();
        }

        if (currentState == moveStatesManager.MoveStates[2])
        {
            CheckState1();
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