using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
    [SerializeField] float speedEnemy = 1f;

    private Rigidbody2D myRigidBody;
    private bool isAlive = true;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isAlive) { return; }
        Move();
    }

    private void Move()
    {
        if (IsFacingLeft())
        {
            myRigidBody.velocity = new Vector2(-speedEnemy, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(speedEnemy, 0f);
        }
    }

    bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2((Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) { return; }
        EnemyDeath(collision);
        GetComponent<Animator>().SetTrigger("IsDie");
        isAlive = false;
        GameSession listEnemy = GameObject.FindGameObjectWithTag("Session").GetComponent<GameSession>();
        listEnemy.ListEnemy.Add(gameObject.name.ToString());
    }
}
