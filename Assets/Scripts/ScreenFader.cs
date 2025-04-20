using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;


    public void FadeToWhite()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        yield return new WaitForSeconds(2f); //  delay before fade

        
        yield return StartCoroutine(Fade(Color.clear, Color.white));

        // hold white
        yield return new WaitForSeconds(1f);

        FindFirstObjectByType<ActiveScene>().ActivateScene(8);

        // back to normal
        yield return StartCoroutine(Fade(Color.white, Color.clear));
    }

    IEnumerator Fade(Color fromColor, Color toColor)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = Color.Lerp(fromColor, toColor, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = toColor;
    }
}
