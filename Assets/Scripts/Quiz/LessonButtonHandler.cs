using UnityEngine;
using UnityEngine.UI;

public class LessonButtonHandler : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject webScreen;
    public GameObject vishingScreen;
    public GameObject becScreen;
    public GameObject authScreen;
    public GameObject ssrfScreen;
    private QuizManager quizManager;

    private void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
        SetupButtonListeners();
    }

    private void SetupButtonListeners()
    {
        // Find and setup buttons in the main screen using Panel names
        SetupModuleButton("Web", "email_web");
        SetupModuleButton("Vishing", "social_engineering");
        SetupModuleButton("BEC", "BEC_and_quishing");
        SetupModuleButton("Auth", "Auth");
        SetupModuleButton("SSRF", "SSRF");
    }

    private void SetupModuleButton(string buttonName, string subcategory)
    {
        // First find the parent GameObject
        Transform moduleParent = mainScreen.transform.Find(buttonName);
        if (moduleParent != null)
        {
            // Find the Button component that's a sibling of the Text component
            Transform buttonTransform = moduleParent.parent.Find("Button");
            if (buttonTransform != null)
            {
                Button button = buttonTransform.GetComponent<Button>();
                if (button != null)
                {
                    Debug.Log($"Found button for {buttonName}");
                    button.onClick.AddListener(() => OnModuleSelected(buttonName, subcategory));
                }
                else
                {
                    Debug.LogWarning($"No Button component found for {buttonName}");
                }
            }
            else
            {
                Debug.LogWarning($"Could not find Button object for {buttonName}");
            }
        }
        else
        {
            Debug.LogWarning($"Could not find Text object named {buttonName}");
        }
    }

    private void OnModuleSelected(string moduleName, string subcategory)
    {
        // Hide main screen
        mainScreen.SetActive(false);

        // Show corresponding module screen
        GameObject moduleScreen = GetModuleScreen(moduleName);
        if (moduleScreen != null)
        {
            moduleScreen.SetActive(true);
        }

        // Setup quiz button listener in the module screen
        Transform quizButton = moduleScreen?.transform.Find("Quiz");
        if (quizButton != null)
        {
            Button button = quizButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => StartQuiz(subcategory));
            }
        }
    }

    private GameObject GetModuleScreen(string moduleName)
    {
        return moduleName switch
        {
            "Web" => webScreen,
            "Vishing" => vishingScreen,
            "BEC" => becScreen,
            "Auth" => authScreen,
            "SSRF" => ssrfScreen,
            _ => null
        };
    }

    private void StartQuiz(string subcategory)
    {
        if (quizManager != null)
        {
            quizManager.InitializeQuiz(subcategory);
        }
    }
}