using Godot;
using System;

// This only works for CharacterBody2D and a separate state machine is neccasary for other bodies.

public partial class Movement_StateMachine : Node
{
    public CharacterBody2D Entity {get; set;} // change this type to work with other physics bodies

    [Export]
    Base_MoveState startState;

    // [Export]
    // Base_MoveState state0;
    [Export]
    Base_MoveState state1;
    [Export]
    Base_MoveState state2;

    Base_MoveState currentState;

    public void Change(Base_MoveState newState)
    {
        currentState.Exit(Entity);
        newState.Enter(Entity);

        currentState = newState;
    }

    public void Init()
    {
        currentState = startState;
        startState.Enter(Entity);
    }


    public void Process()
    {
        currentState.PhysicsProcess(Entity);
    }

    public void PhysicsProcess()
    {
        currentState.Process(Entity);
    }


    // Signal recievers to change move state.

    // private void _on_player_move_state_change_0() // idle state
    // {
    //     Change(state0);
    // }

    private void _on_player_move_state_change_1()
    {
        Change(state1);
    }

    private void _on_player_move_state_change_2()
    {
        Change(state2);
    }
}
