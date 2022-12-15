using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State
{
    public override void Move(float speedRun, float speedCrouch)
    {
        bool isGround = OnLanding(myFeet, Vector2.down);
        bool checkCrouch = OnLanding(myFeet, Vector2.up);
        bool canCrouch = InputSystem.Instance.Crouch() > Mathf.Epsilon;
        float speedMove = 0f;

        if(isGround)
        {
            if(checkCrouch)
            {
                myBody.enabled = false;
                speedMove = speedCrouch;
                myAnimator.SetBool("Is_Crouch", true);
            }
            else
            {
                if(canCrouch)
                {
                    myBody.enabled = false;
                    speedMove = speedCrouch;
                    myAnimator.SetBool("Is_Crouch", true);
                }
                else
                {
                    myBody.enabled = true;
                    speedMove = speedRun;
                    myAnimator.SetBool("Is_Crouch", false);
                }
            }
        }
        else
        {
            myBody.enabled = true;
            speedMove = speedRun;
            myAnimator.SetBool("Is_Crouch", false);
        }

        float moveX = InputSystem.Instance.MoveX() * speedMove;
        Vector2 playerVelocity = new Vector2(moveX, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        myAnimator.SetFloat("Fl_Run", Mathf.Abs(moveX));

        FlipSprite(InputSystem.Instance.MoveX());
    }

    private void FlipSprite(float moveX)
    {
        bool playerSpeed = Mathf.Abs(moveX) > Mathf.Epsilon;
        if (playerSpeed)
        {
            transform.localScale = new Vector2(moveX, 1f);
        }
    }
}