using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDie : State
{
    public override void Die(CharacterController characterController, Vector2 deathKick, AudioClip audioDeath)
    {
        bool isDieByEnviroment = myBody.IsTouchingLayers(LayerMask.GetMask("Hazards")) || myFeet.IsTouchingLayers(LayerMask.GetMask("Hazards"));
        bool isDieByEnemy = myBody.IsTouchingLayers(LayerMask.GetMask("Enemy"));
        bool isDie = isDieByEnviroment || isDieByEnemy;
        if (isDie)
        {
            myAnimator.SetBool("Is_Die", true);
            myAnimator.SetTrigger("Tr_Die");
            myRigidbody2D.velocity = deathKick;
            characterController.enabled = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            AudioDeath(audioDeath);
        }
    }

    private void AudioDeath(AudioClip audioDeath)
    {
        float volume = PlayerPrefsController.GetSFXVolume();
        AudioSource.PlayClipAtPoint(audioDeath, Camera.main.transform.position, volume);
    }
}
