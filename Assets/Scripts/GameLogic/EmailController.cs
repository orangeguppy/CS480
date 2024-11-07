using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EmailController : MonoBehaviour
{
    [Header("Email Content")]
    [SerializeField] private TMP_Text fromText;
    [SerializeField] private TMP_Text toText;
    [SerializeField] private TMP_Text subjectText;
    [SerializeField] private TMP_Text contentText;
    [Header("Buttons")]
    [SerializeField] private Button trueButton;
    [SerializeField] private Button falseButton;

    private EmailManager emailManager;

    private void Awake()
    {
        Debug.Log("[EmailController] Awake called");
        emailManager = GetComponent<EmailManager>();

        if (emailManager == null)
        {
            Debug.LogError("[EmailController] EmailManager component not found!");
        }

        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        if (trueButton != null && falseButton != null)
        {
            trueButton.onClick.RemoveAllListeners();
            falseButton.onClick.RemoveAllListeners();
            trueButton.onClick.AddListener(() => OnAnswerSelected(true));
            falseButton.onClick.AddListener(() => OnAnswerSelected(false));
        }
        else
        {
            Debug.LogError("[EmailController] Button references not set in inspector!");
        }
    }

    public void DisplayEmail(EmailData email)
    {
        if (email == null)
        {
            Debug.LogError("[EmailController] Attempted to display null email data!");
            return;
        }

        Debug.Log("[EmailController] DisplayEmail called");
        fromText.text = email.from;
        toText.text = email.to;
        subjectText.text = email.subject;
        contentText.text = email.content;
        EnableAnswerButtons(true);
    }

    private void OnAnswerSelected(bool answer)
    {
        Debug.Log("[EmailController] Answer selected: " + answer);
        EnableAnswerButtons(false);
        emailManager.HandleAnswer(answer);
        gameObject.SetActive(false);
    }

    private void EnableAnswerButtons(bool enabled)
    {
        if (trueButton != null && falseButton != null)
        {
            trueButton.interactable = enabled;
            falseButton.interactable = enabled;
        }
    }

    private void OnDestroy()
    {
        if (trueButton != null && falseButton != null)
        {
            trueButton.onClick.RemoveAllListeners();
            falseButton.onClick.RemoveAllListeners();
        }
    }
}
