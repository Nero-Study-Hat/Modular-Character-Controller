using Godot;
using System;

public class PlayerMoveStates_Conditions
{
    CharacterBody2D player;
    Movement_StateMachine movement_StateMachine;

    public PlayerMoveStates_Conditions(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager)
    {
        player = EntityRef;
        movement_StateMachine = MoveStatesManager;
    }

    public void ConditionsChecker(Base_MoveState currentState, Base_MoveState[] moveStates) // call in process
    {
        
        if (currentState == movement_StateMachine.MoveStates[0])
        {
            if (CheckState2() == true)
            {
                movement_StateMachine.ChangeState(moveStates[1]);
            }
            return;
        }

        if (currentState == movement_StateMachine.MoveStates[1])
        {
            if (CheckState1() == true)
            {
                movement_StateMachine.ChangeState(moveStates[0]);
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