using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : BasicEnemy
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float timeFrogIdle = 1f;
    [SerializeField] private AudioClip audioIdle;

    private Rigidbody2D myRigidBody;
    private CircleCollider2D myBody;
    private Animator animator;

    private bool isMoveDown = false;
    private bool isFrogIdle = false;
    private float flip;

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
        Move();
    }
    protected override void Move()
    {
        if (isFrogIdle) { return; }
        Vector2 moveLeft = flip > 0f ? Vector2.left : Vector2.right;

        if (!isMoveDown)
        {
            myRigidBody.velocity = ((Vector2.up + moveLeft) * speed);
            animator.SetBool("IsAttackingUp", true);
            animator.SetBool("IsAttackingDown", false);
        }
        else if (!Physics2D.OverlapCircle(transform.position, myBody.radius, LayerMask.GetMask("Foreground")))
        {
            myRigidBody.velocity = ((Vector2.down + moveLeft) * speed);
            animator.SetBool("IsAttackingDown", true);
            animator.SetBool("IsAttackingUp", false);
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
        isFrogIdle = false;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isMoveDown = true;
    }

    private void AudioIdle()
    {
        float volume = PlayerPrefsController.GetSFXVolume();
        AudioSource.PlayClipAtPoint(audioIdle, transform.position, volume);
    }
}