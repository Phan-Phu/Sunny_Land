using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPickup : MonoBehaviour
{
    [SerializeField] int pointCherry = 1;

    [SerializeField] AudioClip cherryPickupSFX;

    bool isAlive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) { return; }

        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Session").GetComponent<GameSession>().AddLives(pointCherry);
            Destroy(gameObject, 0.5f);
            GetComponent<Animator>().SetTrigger("IsDestroy");
            float volume = PlayerPrefsController.GetSFXVolume();
            AudioSource.PlayClipAtPoint(cherryPickupSFX, Camera.main.transform.position, volume);
            isAlive = false;

            GameSession listCherry = FindObjectOfType<GameSession>();
            listCherry.ListCherryItem.Add(gameObject.name.ToString());
        }
    }
}
