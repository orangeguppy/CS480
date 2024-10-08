using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonManager : MonoBehaviour
{
    public GameObject[] lessonScreens;
    private int currentScreenIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all screens to inactive except the first one
        for (int i = 0; i < lessonScreens.Length; i++)
        {
            lessonScreens[i].SetActive(i == currentScreenIndex);
        }
    }

    public void NextScreen()
    {
        HideScreen();
        currentScreenIndex++;

        if (currentScreenIndex >= lessonScreens.Length)
        {
            currentScreenIndex = lessonScreens.Length - 1; 
        }

        lessonScreens[currentScreenIndex].SetActive(true);
    }

    public void PreviousScreen()
    {
        HideScreen();
        currentScreenIndex--;

        if (currentScreenIndex < 0)
        {
            currentScreenIndex = 0; // or wrap around to lessonScreens.Length - 1 if desired
        }

        lessonScreens[currentScreenIndex].SetActive(true);
    }

    public void HideScreen()
    {
        lessonScreens[currentScreenIndex].SetActive(false) ;
    }
}