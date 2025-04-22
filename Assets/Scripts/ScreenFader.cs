using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;


    public void FadeToWhite(bool longerWait, float longerWaitTime, bool GameOver)
    {
        StartCoroutine(FadeSequence(longerWait, longerWaitTime, GameOver));
    }

    IEnumerator FadeSequence(bool longerWait, float longerWaitTime, bool GameOver)
    {
        if (longerWait)
        {
            yield return new WaitForSeconds(longerWaitTime);
        }
        else 
        {
            yield return new WaitForSeconds(2f); //  delay before fade
        }


            

        
        yield return StartCoroutine(Fade(Color.clear, Color.white));
        yield return new WaitForSeconds(1f);

        if (!GameOver)
        {
            FindFirstObjectByType<ActiveScene>().ActivateScene(8);
            yield return StartCoroutine(Fade(Color.white, Color.clear));
        }
        else
        {
            print("gameover");
            FindFirstObjectByType<ActiveScene>().ActivateScene(9);
        }




         
           
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
