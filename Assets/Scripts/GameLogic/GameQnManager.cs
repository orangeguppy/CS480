using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameQnManager : MonoBehaviour
{
    public GameObject question;
    public Image feedbackImage; // Image for feedback
    public float fadeInDuration = 0.3f; // Time for fade-in
    public float fadeOutDuration = 1.0f; // Time for fade-out

    public void CorrectAns()
    {
        question.SetActive(false);
        Time.timeScale = 1f; // Unpause game
        PlayerInfo.LessonScore++;
        ShowFeedback(Color.green);
    }

    public void WrongAns()
    {
        question.SetActive(false);
        Time.timeScale = 1f; // Unpause game
        ShowFeedback(Color.red);
    }

    private void ShowFeedback(Color feedbackColor)
    {
        feedbackImage.color = feedbackColor;
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Ensure the image is enabled and fully transparent to start
        feedbackImage.gameObject.SetActive(true);
        feedbackImage.color = new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, 0);

        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0, 0.2f, elapsedTime / fadeInDuration);
            feedbackImage.color = new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        feedbackImage.color = new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, 1);

        // Immediately start fading out
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(0.2f, 0, elapsedTime / fadeOutDuration);
            feedbackImage.color = new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        feedbackImage.color = new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, 0);

        // Disable the image after fade-out
        feedbackImage.gameObject.SetActive(false);
    }
}
