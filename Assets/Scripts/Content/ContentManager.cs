using UnityEngine;
using System.Collections.Generic;

public class ContentManager : MonoBehaviour
{
    [System.Serializable]
    public class ContentSection
    {
        public string name;
        public GameObject sectionObject;
    }

    public List<ContentSection> contentSections;
    private ContentSection currentActiveSection;

    void Start()
    {
        // Ensure only one section is active at start
        SetActiveSection(contentSections[0].name);
    }

    public void SetActiveSection(string sectionName)
    {
        foreach (var section in contentSections)
        {
            if (section.name == sectionName)
            {
                section.sectionObject.SetActive(true);
                currentActiveSection = section;
            }
            else
            {
                section.sectionObject.SetActive(false);
            }
        }
    }

    public List<string> GetSectionNames()
    {
        List<string> names = new List<string>();
        foreach (var section in contentSections)
        {
            names.Add(section.name);
        }
        return names;
    }
}