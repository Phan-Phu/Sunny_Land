using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : AverageEnemy
{
    [SerializeField] private float speed = 1f;
    [Tooltip("Dimension Y must be != 0")]
    [SerializeField] Vector2 distanceToAttack = new Vector2(5f, 0f);

    private Vector2 direction;
    private Vector2 originPosition;

    private void Start()
    {
        direction = new Vector2(transform.position.x - GetPositionPlayer().x, transform.position.y - GetPositionPlayer().y);
        originPosition = transform.position;
    }

    private void Update()
    {
        if (IsAttack(originPosition, GetPositionPlayer(), distanceToAttack, out Vector2 currentDistance))
        {
            direction = currentDistance;
            Attack();
        }
        else
        {
            ReturnToPositionStart(direction, originPosition, speed);
            if (transform.position.x - originPosition.x == 0)
            {
                Idle();
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
        FlipSprite(direction.x);

        Vector3 dir = transform.position;
        dir.y = originPosition.y;
        transform.position = Vector3.Lerp(dir, GetPositionPlayer(), speed * Time.deltaTime);
    }

    protected override void ReturnToPositionStart(Vector3 direction, Vector3 originPosition, float speed)
    {
        base.ReturnToPositionStart(direction, originPosition, speed);
    }
}
