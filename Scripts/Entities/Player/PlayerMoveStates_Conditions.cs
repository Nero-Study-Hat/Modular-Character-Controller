using Godot;

public class PlayerMoveStates_Conditions
{
    Player player = new Player();
    Movement_StateMachine moveStatesManager = new Movement_StateMachine();

    public void SetClasses()
    {
        player = player.GetPlayerRef();
        moveStatesManager = moveStatesManager.GetMovement_StateMachine();
    }

    public void ConditionsChecker(Base_MoveState currentState, Base_MoveState[] moveStates) // call in process
    {
        if (currentState == moveStatesManager.MoveStates[1])
        {
            if (CheckState2() == true)
            {
                moveStatesManager.ChangeState(moveStates[2]);
            }
            return;
        }

        if (currentState == moveStatesManager.MoveStates[2])
        {
            if (CheckState1() == true)
            {
                moveStatesManager.ChangeState(moveStates[1]);
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