using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Animator myAnimator;
    protected Rigidbody2D myRigidbody2D;
    protected BoxCollider2D myBody;
    protected CircleCollider2D myFeet;

    private float startGravity;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<CircleCollider2D>();
        myBody = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        startGravity = myRigidbody2D.gravityScale;
    }

    public virtual void Move(float speedRun, float speedCrouch)
    {

    }

    public virtual void Climb(float speed)
    {

    }

    public virtual void Jump(float jumpForce)
    {

    }

    public virtual void Die(CharacterController characterController, Vector2 deathKick, AudioClip audioDeath)
    {

    }
    protected bool CheckLayer(CircleCollider2D circleCollider2D, LayerMask layerMask)
    {
        Vector2 position = new Vector2(transform.position.x + circleCollider2D.offset.x, transform.position.y + circleCollider2D.offset.y);
        bool checkForeground = Physics2D.OverlapCircle(position, circleCollider2D.radius, layerMask);
        return checkForeground;
    }

    protected bool CheckRaycastCollision(CircleCollider2D circleCollider2D, Vector2 direction)
    {
        float maxDistance = 1f;

        Vector2 startPosition = circleCollider2D.transform.position;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(startPosition, direction, maxDistance, LayerMask.GetMask("Foreground"));

        if (raycastHit2D.collider != null)
        {
            return true;
        }
        return false;
    }

    protected bool CheckCollisionLayer(CircleCollider2D circleCollider2D, LayerMask layerMask)
    {
        Vector2 point = circleCollider2D.transform.position;
        float radius = 1;
        bool onLanding = Physics2D.OverlapCircle(point, radius, layerMask);
        if(onLanding)
        {
            return true;
        }
        return false;
    }

    protected float GetGravityStart()
    {
        return startGravity;
    }
}
