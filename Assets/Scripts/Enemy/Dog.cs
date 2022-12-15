using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Enemy
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distanceToAttack = 5f;
    private bool isAttackLeft;

    private Animator animator;

    private Vector3 originPosition;
    private bool isAlive;
    private bool isGaze;
    private bool isAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Idle();
        isAlive = true;
        isGaze = false;
        originPosition = transform.position;
        isAttackLeft = transform.localScale.x > 0 ? true : false;
    }

    private void Update()
    {
        if (!isAlive) { return; }

        Vector2 currentPosition = originPosition - player.transform.position;
        isAttack = SetIsAttackLeft(currentPosition);

        if (transform.position == originPosition && !isAttack)
        {
            Idle();
        }
        else
        {
            Attack(currentPosition);
        }
    }

    private bool SetIsAttackLeft(Vector3 currentPosition)
    {
        if (isAttackLeft)
        {
            return currentPosition.x < distanceToAttack && currentPosition.x > 0;
        }
        return currentPosition.x > -distanceToAttack && currentPosition.x < 0;
    }

    private void Idle()
    {
        animator.SetBool("IsAttack", false);
    }

    private void Attack(Vector3 direction)
    {
        animator.SetBool("IsAttack", true);

        if (isAttack)
        {
            // Enemy attack
            FilpSprite(direction.x);

            Vector3 dir = transform.position;
            dir.y = originPosition.y;
            transform.position = Vector3.Lerp(dir, player.transform.position, speed * Time.deltaTime);
            isGaze = true;
        }
        else
        {
            if (isGaze == true)
            {
                StartCoroutine(LookEnemy());
            }
            else
            {
                ReturnToPositionStart(direction);
            }
        }
    }

    private void ReturnToPositionStart(Vector3 direction)
    {
        Vector3 dir = transform.position;
        dir.y = originPosition.y;
        transform.position = Vector3.MoveTowards(dir, originPosition, speed * Time.deltaTime);
        FilpSprite(-direction.x);

        // Enemy done return
        if (transform.position == originPosition)
        {
            FilpSprite(direction.x);
        }
    }

    private void FilpSprite(float direction)
    {
        float flip = Mathf.Sign(direction);
        transform.localScale = new Vector2(flip, 1);
    }

    private IEnumerator LookEnemy()
    {
        Idle();
        float timeLookEnemy = 1f;
        yield return new WaitForSeconds(timeLookEnemy);
        isGaze = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isAlive) { return; }
        isAlive = false;
        EnemyDeath(collision);
        animator.SetTrigger("IsDie");

        GameSession listEnemy = FindObjectOfType<GameSession>();
        listEnemy.ListEnemy.Add(gameObject.name.ToString());
    }
}
