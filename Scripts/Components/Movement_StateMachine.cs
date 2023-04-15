using Godot;
using System;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class Movement_StateMachine : Node
{
    public CharacterBody2D Entity {get; set;} // change this type to work with other physics bodies

    [Export]
    Base_MoveState startState;

    PlayerMoveStates_Conditions moveStates_Conditions;

    // [Export]
    // Base_MoveState state0;
    // [Export]
    // Base_MoveState state1;
    // [Export]
    // Base_MoveState state2;

    public Base_MoveState CurrentState {get; private set;}
    public Base_MoveState[] MoveStates {get; private set;}

    public Movement_StateMachine GetMovement_StateMachine()
    {
        return this;
    }

    [Signal]
    public delegate void MoveState_ChangedEventHandler(Base_MoveState MoveState);

    public void Change(Base_MoveState newState)
    {
        CurrentState.Exit(Entity);
        newState.Enter(Entity);

        CurrentState = newState;
        EmitSignal(SignalName.MoveState_Changed, newState);
    }


    public void Init(Player player)
    {
        moveStates_Conditions = new PlayerMoveStates_Conditions(player, this, CurrentState);

        var numStates = this.GetChildCount();
        MoveStates = new Base_MoveState[numStates];

        for (int index = 0; index < numStates; index++)
        {
            if (this.GetChild(index) is Base_MoveState)
            {
                MoveStates[index] = this.GetChild<Base_MoveState>(index);
            }
        }

        CurrentState = startState;
        startState.Enter(Entity);
    }


    public void Process()
    {
        CurrentState.Process(Entity);
    }

    public void PhysicsProcess()
    {
        CurrentState.PhysicsProcess(Entity);
    }


    // Signal recievers to change move state.

    // private void _on_player_move_state_change_0() // idle state
    // {
    //     Change(state0);
    // }

    private void _on_player_move_state_change_1()
    {
        Change(MoveStates[0]);
    }

    private void _on_player_move_state_change_2()
    {
        Change(MoveStates[1]);
    }
}
