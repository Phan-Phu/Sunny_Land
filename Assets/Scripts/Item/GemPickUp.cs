using UnityEngine;
using UnityEngine.SceneManagement;

public class GemPickUp : MonoBehaviour
{
    [SerializeField] int pointGem;
    [SerializeField] AudioClip gemPickupSFX;

    bool isAlive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) { return; }

        if (other.CompareTag("Player"))
        {
            GameSession.Instance.AddToScore(pointGem);
            Destroy(gameObject, 0.5f);
            GetComponent<Animator>().SetTrigger("IsDestroy");
            isAlive = false;

            // get value volume from data
            float volume = PlayerPrefsController.GetSFXVolume();
            AudioSource.PlayClipAtPoint(gemPickupSFX, Camera.main.transform.position, volume);

            // save data when reload scene (not save in local)
            GameSession listGem = FindObjectOfType<GameSession>();
            listGem.ListGemItem.Add(gameObject.name.ToString());
        }
    }
}
