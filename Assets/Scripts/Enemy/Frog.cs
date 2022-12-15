using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float DistanceToAttack = 9f;
    [SerializeField] private float timeFrogIdle = 1f;
    [SerializeField] private AudioClip audioIdle;

    private Rigidbody2D myRigidBody;
    private CircleCollider2D myBody;
    private Animator animator;

    private bool isMoveDown = false;
    private bool isFrogIdle = false;
    private float flip;
    private bool isAlive = true;
    private bool completeTurn = true;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBody = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        flip = transform.localScale.x;
    }

    void FixedUpdate()
    {
        if (!isAlive) { return; }

        if (!player) { return; }
        bool isAttacking = (transform.position.x - player.transform.position.x) < DistanceToAttack;
        if (!isAttacking && completeTurn)
        {
            animator.SetBool("IsAttackingDown", false);
            animator.SetBool("IsAttackingUp", false);
        }
        else
        {
            Move();
        }
    }
    private void Move()
    {
        if (isFrogIdle) { return; }
        Vector2 moveLeft = flip > 0f ? Vector2.left : Vector2.right;

        if (!isMoveDown)
        {
            myRigidBody.velocity = ((Vector2.up + moveLeft) * speed);
            animator.SetBool("IsAttackingUp", true);
            animator.SetBool("IsAttackingDown", false);
            completeTurn = false;
        }
        else if (!Physics2D.OverlapCircle(transform.position, myBody.radius, LayerMask.GetMask("Foreground")))
        {
            myRigidBody.velocity = ((Vector2.down + moveLeft) * speed);
            animator.SetBool("IsAttackingDown", true);
            animator.SetBool("IsAttackingUp", false);
            completeTurn = false;
        }
        else if (Physics2D.OverlapCircle(transform.position, myBody.radius, LayerMask.GetMask("Foreground")))
        {
            isMoveDown = false;
            flip = -flip;
            transform.localScale = new Vector2(-Mathf.Sign(-flip), 1);
            StartCoroutine(FrogIdle());
        }
    }
    private IEnumerator FrogIdle()
    {
        myRigidBody.velocity = ((Vector2.down + Vector2.left) * 0);
        animator.SetBool("IsAttackingDown", false);
        animator.SetBool("IsAttackingUp", false);
        AudioIdle();
        isFrogIdle = true;
        yield return new WaitForSeconds(timeFrogIdle);
        completeTurn = true;
        isFrogIdle = false;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isMoveDown = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) { return; }

        EnemyDeath(collision);
        animator.SetTrigger("IsDie");
        isAlive = false;
        GameSession listEnemy = FindObjectOfType<GameSession>();
        listEnemy.ListEnemy.Add(gameObject.name.ToString());
    }

    private void AudioIdle()
    {
        float volume = PlayerPrefsController.GetSFXVolume();
        AudioSource.PlayClipAtPoint(audioIdle, Camera.main.transform.position, volume);
    }
}