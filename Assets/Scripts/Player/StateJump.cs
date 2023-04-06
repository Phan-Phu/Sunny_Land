using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateJump : State
{
    public override void Jump(float jumpForce)
    {
        bool isClimb = myFeet.IsTouchingLayers(LayerMask.GetMask("Climb"));
        bool isGround = OnLanding(myFeet, Vector2.down);
        bool canJump = InputSystem.Instance.Crouch() == 0;

        myAnimator.SetBool("Is_Ground", isGround);

        if (isClimb)
        {
            myAnimator.SetBool("Is_Ground", true);
            myAnimator.SetBool("Is_Jump", false);
        }

        if (!isGround || !canJump || isClimb)
        {
            return;
        }

        float isJumping = InputSystem.Instance.Jump();
        float jump = isJumping > 0 ? isJumping * jumpForce : myRigidbody2D.velocity.y;

        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jump);
        myAnimator.SetBool("Is_Jump", isJumping > Mathf.Epsilon);
    }
}
