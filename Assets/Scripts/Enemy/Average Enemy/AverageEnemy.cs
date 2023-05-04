using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageEnemy : Enemy
{
    protected Animator animator;

    private bool isAlive = true;
    protected bool isAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual bool IsAttack(Vector2 originPosition, Vector2 positionPlayer, Vector2 distanceToAttack, out Vector2 currentDistance)
    {
        float currentDistanceX = originPosition.x - positionPlayer.x;
        bool isAttackX = Mathf.Abs(currentDistanceX) < distanceToAttack.x ? true : false;

        float currentDistanceY = originPosition.y - positionPlayer.y;
        bool isAttackY = Mathf.Abs(currentDistanceY) < distanceToAttack.y ? true : false;

        currentDistance = new Vector2(currentDistanceX, currentDistanceY);

        bool isAttack = isAttackX && isAttackY;
        return isAttack;
    }

    protected virtual void Update()
    {
        if(!isAlive)
        {
            return;
        }
    }

    protected virtual void Idle()
    {
        animator.SetBool("IsAttack", false);
    }

    protected virtual void Attack()
    {

    }

    protected virtual void ReturnToPositionStart(Vector3 direction, Vector3 originPosition, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, originPosition, speed * Time.deltaTime);
        FlipSprite(-direction.x);

        // Enemy done return
        if (transform.position == originPosition)
        {
            FlipSprite(direction.x);
        }
    }

    protected void FlipSprite(float direction)
    {
        float flip = Mathf.Sign(direction);
        transform.localScale = new Vector2(flip, 1);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) { return; }

        EnemyDeath(collision);
        animator.SetTrigger("IsDie");
        isAlive = false;
        GameSession listEnemy = FindObjectOfType<GameSession>();
        listEnemy.ListEnemy.Add(gameObject.name.ToString());
    }
}
