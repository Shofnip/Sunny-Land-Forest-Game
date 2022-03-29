using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
  [SerializeField] private Image fadeImage;
  [SerializeField] private Color initialFadeColor;
  [SerializeField] private Color finalFadeColor;
  [SerializeField] private float fadeDurationSeconds;

  private float time;

  void Start()
  {
    StartCoroutine("StartFade");
  }

  IEnumerator StartFade() {
    time = 0f;

    while( time <= fadeDurationSeconds ) {
      fadeImage.color = Color.Lerp(initialFadeColor, finalFadeColor, time / fadeDurationSeconds);
      time += Time.deltaTime;
      yield return null;
    }
  }
}
