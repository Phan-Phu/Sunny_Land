using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected CharacterController player;
    [SerializeField] protected Vector2 killKickPlayer = new Vector2(0f, 10f);
    [SerializeField] protected AudioClip enemyDeathSFX;

    protected void EnemyDeath(Collision2D collision)
    {
        if(IsPlayer(collision.collider))
        {
            Destroy(gameObject, 0.5f);
            player.GetComponent<Rigidbody2D>().velocity = killKickPlayer;
            AudioDeath();
        }
    }

    protected bool IsPlayer(Collider2D collider2D)
    {
        float maxDistance = 1f;

        Vector2 startPosition = collider2D.transform.position;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(startPosition, Vector2.up, maxDistance, LayerMask.GetMask("Player"));

        if (raycastHit2D.collider != null)
        {
            return true;
        }
        return false;
    }

    private void AudioDeath()
    {
        float volume = PlayerPrefsController.GetSFXVolume();
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position, volume);
    }
}
