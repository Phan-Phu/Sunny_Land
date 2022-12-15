using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextTutorial : MonoBehaviour
{
    Animator animator;
    BoxCollider2D myBody;
    [SerializeField] private string text;
    [SerializeField] private float TimeToResetText = 3f;

    private Image frameTutorial;
    private TextMeshProUGUI textTutorial;
    public bool boolTouch { get; private set; }
    private bool isUse = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        myBody = GetComponent<BoxCollider2D>();
        frameTutorial = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Image>();
        textTutorial = frameTutorial.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentIndex == 1)
        {
            string text = "Press A (Left Arrow) Or D (Right Arrow) to move";
            StartCoroutine(SetString(text));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("Player")) && isUse == false)
        {
            animator.SetBool("IsChecked", true);
            boolTouch = true;
            StartCoroutine(SetString(text));
        }
    }

    private IEnumerator SetString(string text)
    {
        isUse = true;
        frameTutorial.enabled = true;
        textTutorial.text = text.ToString();

        yield return new WaitForSeconds(TimeToResetText);
        animator.SetBool("IsChecked", false);
        boolTouch = true;
        textTutorial.text = "";
        frameTutorial.enabled = false;
        isUse = false;
    }
}
