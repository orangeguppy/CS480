using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailController : MonoBehaviour
{
    private TMP_Text fromText;
    private TMP_Text toText;
    private TMP_Text subjectText;
    private TMP_Text contentText;
    private Button trueButton;
    private Button falseButton;

    private EmailManager manager;

    private void Awake()
    {
        trueButton.onClick.AddListener(() => OnAnswerSelected(true));
        falseButton.onClick.AddListener(() => OnAnswerSelected(false));
        gameObject.SetActive(false);
    }

    public void Initialize(EmailManager emailManager)
    {
        manager = emailManager;
    }

    public void DisplayEmail(EmailData email)
    {
        fromText.text = email.from;
        toText.text = email.to;
        subjectText.text = email.subject;
        contentText.text = email.content;
        EnableAnswerButtons(true);
    }

    private void OnAnswerSelected(bool answer)
    {
        EnableAnswerButtons(false);
        manager.HandleAnswer(answer);
    }

    public void ShowEmailMinigame()
    {
        gameObject.SetActive(true);
    }

    public void HideEmailMinigame()
    {
        gameObject.SetActive(false);
    }

    private void EnableAnswerButtons(bool enabled)
    {
        trueButton.interactable = enabled;
        falseButton.interactable = enabled;
    }

    private void OnDestroy()
    {
        trueButton.onClick.RemoveAllListeners();
        falseButton.onClick.RemoveAllListeners();
    }
}