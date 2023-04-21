using Godot;
using System.Collections.Generic;

public partial class Player_MoveSwitchCheck : BaseSwitchCheck
{
    CharacterBody2D player = new CharacterBody2D();
    Movement_StateMachine movement_StateMachine = new Movement_StateMachine();

    MoveStateFactory.MoveStates enumVal_CurrentState;
    Dictionary<MoveStateFactory.MoveStates, Base_MoveState> entityStateDict = new Dictionary<MoveStateFactory.MoveStates, Base_MoveState>();

    // Idea, try making an action dictionary with callables
    // and a switch statement with the dictionary keys and
    // current state name.

    // Dictionary<MoveStateFactory.MoveStates, Base_MoveState> actionDictionary = new Dictionary<MoveStateFactory.MoveStates, Base_MoveState>();

    public override void Initialize(CharacterBody2D EntityRef, Movement_StateMachine MoveStatesManager)
    {
        player = EntityRef;
        movement_StateMachine = MoveStatesManager;
        
        entityStateDict = movement_StateMachine.entityMoveStatesDict;
    }

    public override void ConditionsChecker()
    {
        switch (movement_StateMachine.EnumVal_CurrentState)
        {
            case MoveStateFactory.MoveStates.BIdle_MoveState:
                BIdleCheck();
                break;

            case MoveStateFactory.MoveStates.BNormal_MoveState:
                BNormalCheck();
                break;
        }
    }


    private void BIdleCheck()
    {
        if (Input.IsActionJustPressed("moveState_run"))
        {
            movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BNormal_MoveState]);
        }
    }

    private void BNormalCheck()
    {
        if (Input.IsActionJustPressed("moveState_walk"))
        {
            movement_StateMachine.ChangeState(entityStateDict[MoveStateFactory.MoveStates.BIdle_MoveState]);
        }
    }
}
