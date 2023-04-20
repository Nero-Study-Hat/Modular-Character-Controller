using Godot;
using System;

public class PlayerMoveStates_SwitchCheck:ISwitchMoveStates_Check
{
    CharacterBody2D player;
    Movement_StateMachine movement_StateMachine;

    Base_MoveState currentState;
    Base_MoveState[] moveStates;

    public void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager)
    {
        player = EntityRef;
        movement_StateMachine = MoveStatesManager;
        
        currentState = movement_StateMachine.CurrentState;
        moveStates = movement_StateMachine.MoveStates;
    }

    public void ConditionsChecker() // call in process
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