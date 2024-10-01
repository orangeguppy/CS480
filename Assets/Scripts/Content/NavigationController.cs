using UnityEngine;
using UnityEngine.UI;

public class NavigationController : MonoBehaviour
{
    public ContentManager contentManager;
    public Transform buttonContainer;
    public GameObject buttonPrefab;

    void Start()
    {
        CreateNavigationButtons();
    }

    void CreateNavigationButtons()
    {
        foreach (string sectionName in contentManager.GetSectionNames())
        {
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = sectionName;
            button.onClick.AddListener(() => OnSectionButtonClick(sectionName));
        }
    }

    void OnSectionButtonClick(string sectionName)
    {
        contentManager.SetActiveSection(sectionName);
    }
}