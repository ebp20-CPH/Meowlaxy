using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] Image blackScreen;

    float timeToFade = 1f;

    private void Start()
    {
        StartFade();
    }

    public void StartFade() 
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void StartFadeIn() 
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() 
    {
        float timer = timeToFade;

        while (timer > 0) 
        {
            timer -= Time.deltaTime;
            blackScreen.material.color = new Color(1, 1, 1, timer / timeToFade);

            yield return null;
        }

        blackScreen.material.color = new Color(1, 1, 1, 0);
    }

    IEnumerator FadeOut()
    {
        float timer = timeToFade;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            blackScreen.material.color = new Color(1, 1, 1, 1 - timer / timeToFade);

            yield return null;
        }

        blackScreen.material.color = new Color(1, 1, 1, 1);
    }
}
