using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    Animator animator;
    BoxCollider2D myBody;

    public bool boolTouch { get; private set; }
    private int startScene;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        myBody = GetComponent<BoxCollider2D>();
        int numSavePoints = FindObjectsOfType<SavePoint>().Length;
        if (numSavePoints > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != startScene)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            animator.SetBool("IsChecked", true);
            boolTouch = true;
        }
    }
}
