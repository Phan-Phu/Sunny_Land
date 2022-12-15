using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedRun = 10f;
    [SerializeField] private float speedCrouch = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float speedClimb = 7f;
    [SerializeField] private Vector2 deathKick = new Vector2(0,10);
    [SerializeField] private AudioClip audioDeath;

    private State[] stateArray;

    private void Awake()
    {
        stateArray = GetComponents<State>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Climb();
        Die();
    }

    private void Move()
    {
        StateMove stateMove = GetState<StateMove>();
        stateMove.Move(speedRun, speedCrouch);
    }

    private void Jump()
    {
        StateJump stateJump = GetState<StateJump>();
        stateJump.Jump(jumpForce);
    }

    private void Climb()
    {
        StateClimb stateClimb = GetState<StateClimb>();
        stateClimb.Climb(speedClimb);
    }

    private void Die()
    {
        StateDie stateDie = GetState<StateDie>();
        stateDie.Die(this, deathKick, audioDeath);
    }

    private T GetState<T>() where T : State
    {
        foreach (State state in stateArray)
        {
            if (state is T)
            {
                return (T)state;
            }
        }
        return null;
    }
}
