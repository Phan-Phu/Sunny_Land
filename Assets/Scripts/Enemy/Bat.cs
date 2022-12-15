using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [SerializeField] GameObject gameObjectIdle;
    [SerializeField] GameObject gameObjectAttack;
    [SerializeField] private Vector2 distanceToAttack = Vector2.zero;
    [SerializeField] float speed = 5f;

    private Vector3 originPosition;
    private Animator animatorIdle;
    private Animator animatorAttack;

    bool isAlive = true;
    bool isDetect = false;

    private void Awake()
    {
        animatorIdle = gameObjectIdle.GetComponent<Animator>();
        animatorAttack = gameObjectAttack.GetComponent<Animator>();
    }

    private void Start()
    {
        Idle();
        originPosition = transform.position;
    }

    private void Update()
    {
        if (!isAlive) { return; }

        Vector2 currentPos = originPosition - player.transform.position;
        bool isAttackX = Mathf.Abs(currentPos.x) < distanceToAttack.x;
        bool isAttackY = Mathf.Abs(currentPos.y) < distanceToAttack.y;

        if (transform.position == originPosition && (!isAttackX ||!isAttackY))
        {
            Idle(isAttackX, isAttackY);
        }
        else
        {
            Attack(isAttackX, isAttackY, currentPos);
        }
    }

    private void Idle()
    {
        gameObjectIdle.SetActive(true);
        gameObjectAttack.SetActive(false);
    }

    private void Idle(bool isAttackX, bool isAttackY)
    {
        gameObjectIdle.SetActive(true);
        gameObjectAttack.SetActive(false);

    }

    private void Attack(bool isAttackX, bool isAttackY, Vector2 currentPos)
    {
        if (!player) { return; }

        gameObjectIdle.SetActive(false);
        gameObjectAttack.SetActive(true);

        if (isAttackX && isAttackY)
        {
            if(isDetect == false)
            {
                Idle();
                StartCoroutine(Sleep());
            }
            else
            {
                FilpSprite(currentPos.x);
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            ReturnStartPosition(currentPos);
        }
    }

    private void ReturnStartPosition(Vector3 currentPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, originPosition, speed * Time.deltaTime);
        FilpSprite(-currentPos.x);
        if (transform.position == originPosition)
        {
            FilpSprite(currentPos.x);
        }
        isDetect = false;
    }

    private IEnumerator Sleep()
    {
        float timeSleep = 3f;
        yield return new WaitForSeconds(timeSleep);
        isDetect = true;
    }

    private void FilpSprite(float direction)
    {
        float flip = Mathf.Sign(direction);
        gameObjectAttack.transform.localScale = new Vector2(flip, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isAlive = false;
        EnemyDeath(collision);
        animatorIdle.SetTrigger("IsDie");
        animatorAttack.SetTrigger("IsDie");

        GameSession listEnemy = FindObjectOfType<GameSession>();
        listEnemy.ListEnemy.Add(gameObject.name.ToString());
    }
}
