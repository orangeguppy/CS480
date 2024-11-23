using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image black;
    public AnimationCurve fadeCurve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        while(t > 0f)
        {
            t -= Time.deltaTime * 0.5f;
            black.color = new Color(0f, 0f, 0f, fadeCurve.Evaluate(t));
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            black.color = new Color(0f, 0f, 0f, fadeCurve.Evaluate(t));
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
