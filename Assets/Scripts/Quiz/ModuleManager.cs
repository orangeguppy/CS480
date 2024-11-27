using UnityEngine;
using System.Collections.Generic;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager Instance { get; private set; }

    private Dictionary<string, string> moduleSubcategories = new Dictionary<string, string>
    {
        { "Web", "email_web" },
        { "Vishing", "social_engineering" },
        { "BEC", "BEC_and_quishing" },
        { "Auth", "Auth" },
        { "SSRF", "SSRF" }
    };

    private string currentModule;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentModule(string moduleName)
    {
        currentModule = moduleName;
    }

    public string GetCurrentSubcategory()
    {
        return moduleSubcategories.TryGetValue(currentModule, out string subcategory) ? subcategory : "";
    }
}