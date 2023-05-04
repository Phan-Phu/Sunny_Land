using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : AverageEnemy
{
    [SerializeField] float speed = 3f;
    [SerializeField] Vector2 distanceToAttack = new Vector2(9f, 3f);
    [SerializeField] AudioClip audioAttack;

    private Vector2 originPosition;
    private Vector2 direction;
    private bool isPlaying = false;

    private void Start()
    {
        direction = new Vector2(transform.position.x - GetPositionPlayer().x, transform.position.y - GetPositionPlayer().y);
        originPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if (IsAttack(originPosition, GetPositionPlayer(), distanceToAttack, out Vector2 currentDistance))
        {
            AudioAttack();
            direction = currentDistance;
            Attack();
        }
        else
        {
            ReturnToPositionStart(direction, originPosition, speed);
            isPlaying = false;
        }
    }

    protected override void Attack()
    {
        FlipSprite(direction.x);
        transform.position = Vector3.MoveTowards(transform.position, GetPositionPlayer(), speed * Time.deltaTime);
    }

    protected override bool IsAttack(Vector2 originPosition, Vector2 positionPlayer, Vector2 distanceToAttack, out Vector2 currentDistance)
    {
        return base.IsAttack(originPosition, positionPlayer, distanceToAttack, out currentDistance);
    }

    protected override void ReturnToPositionStart(Vector3 direction, Vector3 originPosition, float speed)
    {
        base.ReturnToPositionStart(direction, originPosition, speed);
    }

    private void AudioAttack()
    {
        if (isPlaying) { return; }
        float volume = PlayerPrefsController.GetSFXVolume();
        AudioSource.PlayClipAtPoint(audioAttack, Camera.main.transform.position, volume);
        isPlaying = true;
    }
}
