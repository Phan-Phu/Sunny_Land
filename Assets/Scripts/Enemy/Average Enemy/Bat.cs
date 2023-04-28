using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : AverageEnemy
{
    [SerializeField] private float speed = 1f;
    [SerializeField] Vector2 distanceToAttack = new Vector2(5f, 5f);

    private Vector2 direction;
    private Vector2 originPosition;
    private bool isFreeze;
    [SerializeField] private float timeFreeze = 0;

    private void Start()
    {
        direction = new Vector2(transform.position.x - GetPositionPlayer().x, transform.position.y - GetPositionPlayer().y);
        originPosition = transform.position;
        isFreeze = false;
    }

    protected override void Update()
    {
        base.Update();

        if (IsAttack(originPosition, GetPositionPlayer(), distanceToAttack, out Vector2 currentDistance) && !isFreeze)
        {
            direction = currentDistance;
            Attack();
            timeFreeze = 0;
        }
        else
        {
            ReturnToPositionStart(-direction, originPosition, speed);
            if (transform.position.x - originPosition.x == 0 && transform.position.y - originPosition.y == 0)
            {
                isFreeze = true;
                Idle();
                timeFreeze += Time.deltaTime;
                if(timeFreeze >= 3f)
                {
                    isFreeze = false;
                }
            }
        }
    }

    protected override bool IsAttack(Vector2 originPosition, Vector2 positionPlayer, Vector2 distanceToAttack, out Vector2 currentDistance)
    {
        return base.IsAttack(originPosition, positionPlayer, distanceToAttack, out currentDistance);
    }

    protected override void Idle()
    {
        base.Idle();
    }

    protected override void Attack()
    {
        animator.SetBool("IsAttack", true);

        // Enemy attack
        FlipSprite(-direction.x);
        transform.position = Vector3.MoveTowards(transform.position, GetPositionPlayer(), speed * Time.deltaTime);
    }

    protected override void ReturnToPositionStart(Vector3 direction, Vector3 originPosition, float speed)
    {
        base.ReturnToPositionStart(direction, originPosition, speed);
    }
}
