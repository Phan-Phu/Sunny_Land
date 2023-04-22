using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private bool isAlive = true;

    void FixedUpdate()
    {
        if (!isAlive) { return; }
        Move();
    }

    protected virtual void Move()
    {

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) { return; }
        EnemyDeath(collision);
        GetComponent<Animator>().SetTrigger("IsDie");
        isAlive = false;
        GameSession listEnemy = GameObject.FindGameObjectWithTag("Session").GetComponent<GameSession>();
        listEnemy.ListEnemy.Add(gameObject.name.ToString());
    }
}
