using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateClimb : State
{
    public override void Climb(float speed)
    {
        bool canCimb = CheckLayer(myFeet, LayerMask.GetMask("Climb"));

        if (!canCimb)
        {
            myAnimator.SetFloat("Fl_Climb", 0);
            myAnimator.SetBool("Is_Climb", false);
            myRigidbody2D.gravityScale = GetGravityStart();
            return;
        }

        float climb = InputSystem.Instance.MoveY();
        Vector2 climbVelocity = new Vector2(myRigidbody2D.velocity.x, climb * speed);
        myRigidbody2D.velocity = climbVelocity;
        myRigidbody2D.gravityScale = 0f;

        myAnimator.SetFloat("Fl_Climb", Mathf.Abs(climb));

        if (CheckRaycastCollision(myFeet, Vector2.down, LayerMask.GetMask("Foreground")) || !myBody.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            myAnimator.SetBool("Is_Climb", false);
        }
        else
        {
            myAnimator.SetBool("Is_Climb", true);
        }
    }
}
