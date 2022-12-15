using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField] float speed = 3f;
    [SerializeField] Vector2 distanceToAttack = new Vector2(9f, 3f);
    [SerializeField] AudioClip audioAttack;

    private Vector3 originPosition;

    bool isAlive = true;
    bool isAttack = false;

    private void Start()
    {
        originPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!isAlive) { return; }
        Attack();
    }

    private void Attack()
    {
        if (player)
        {
            Vector2 currentPos = transform.localPosition - player.transform.position;
            bool isAttackX = currentPos.x < distanceToAttack.x;
            bool isAttackY = currentPos.y < distanceToAttack.y;

            if (isAttackX && isAttackY)
            {
                FilpSprite(currentPos.x);
                AudioAttack();
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                ReturnToPositionStart(currentPos);
            }
        }
    }

    private void ReturnToPositionStart(Vector2 currentPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, originPosition, speed * Time.deltaTime);
        FilpSprite(-currentPos.x);
        if (transform.position == originPosition)
        {
            FilpSprite(currentPos.x);
        }
        isAttack = false;
    }

    private void AudioAttack()
    {
        if(isAttack) { return; }
        float volume = PlayerPrefsController.GetSFXVolume();
        AudioSource.PlayClipAtPoint(audioAttack, Camera.main.transform.position, volume);
        isAttack = true;
    }

    private void FilpSprite(float direction)
    {
        float flip = Mathf.Sign(direction);
        transform.localScale = new Vector2(flip, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) { return; }
        isAlive = false;
        EnemyDeath(collision);
        GetComponent<Animator>().SetTrigger("IsDie");
        GameSession listEnemy = FindObjectOfType<GameSession>();
        listEnemy.ListEnemy.Add(gameObject.name.ToString());
    }
}
