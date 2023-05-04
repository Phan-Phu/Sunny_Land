using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slug : BasicEnemy
{
    [SerializeField] float speed = 1f;

    private Rigidbody2D myRigidBody;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    protected override void Move()
    {
        if (IsFacingLeft())
        {
            myRigidBody.velocity = new Vector2(-speed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(speed, 0f);
        }
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2((Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
