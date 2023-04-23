using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeLoadScene : MonoBehaviour
{
    [SerializeField] private float limitfadeLoadImage = 3f;
    private Image fadeLoadImage;
    float startAlpha;

    private void Awake()
    {
        fadeLoadImage = GetComponent<Image>();
        startAlpha = fadeLoadImage.color.a;
    }

    public void FadeInScene()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    public void FadeOutScene()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float time = 0;
        float alpha = 0;

        while (alpha < startAlpha)
        {
            time += Time.deltaTime;
            alpha = startAlpha * time / limitfadeLoadImage;
            fadeLoadImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        float time = limitfadeLoadImage;
        float alpha = startAlpha;

        while (alpha > 0)
        {
            time -= Time.deltaTime;
            alpha = startAlpha * time / limitfadeLoadImage;
            if(alpha < 0) { alpha = 0; }
            fadeLoadImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
}
