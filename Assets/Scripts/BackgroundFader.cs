using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _nightRenderer;
    [SerializeField] private float _fadeDuration =2f;

    public IEnumerator FadeToDay()
    {
        float currentTime = 0;

        while (currentTime < _fadeDuration)
        {
            currentTime += Time.deltaTime;
            Color tempcolor = _nightRenderer.color;
            tempcolor.a = Mathf.Lerp(1, 0, currentTime / _fadeDuration);
            _nightRenderer.color = tempcolor;
            yield return null;
        }
    }

    public IEnumerator FadeToNight()
    {
        float currentTime = 0;

        while (currentTime < _fadeDuration)
        {
            currentTime += Time.deltaTime;
            Color tempcolor = _nightRenderer.color;
            tempcolor.a = Mathf.Lerp(0, 1, currentTime / _fadeDuration);
            _nightRenderer.color = tempcolor;
            yield return null;
        }
    }
}
