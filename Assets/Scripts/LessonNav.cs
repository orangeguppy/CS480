using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LessonNav : MonoBehaviour
{
    public GameObject lessonNavMenu;

    public GameObject webLessonMenu;
    public GameObject vishLessonMenu;
    public GameObject becLessonMenu;
    public GameObject authLessonMenu;
    public GameObject ssrfLessonMenu;

    private GameObject targetMenu;
    public SceneFader sceneFader;

    public Button[] lessonButtons;
    
    void Start()
    {
        int highestLesson = PlayerPrefs.GetInt("highestLesson", 1);
        for (int i = 0; i < lessonButtons.Length; i++)
        {
            if( i+1 > highestLesson)
            {
                lessonButtons[i].interactable = false;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWebMenu()
    {
        ShowLessonMenu(webLessonMenu);
    }

    public void ShowVishMenu()
    {
        ShowLessonMenu(vishLessonMenu);
    }

    public void ShowBECMenu()
    {
        ShowLessonMenu(becLessonMenu);
    }

    public void ShowAuthMenu()
    {
        ShowLessonMenu(authLessonMenu);
    }

    public void ShowSSRFMenu()
    {
        ShowLessonMenu(ssrfLessonMenu);
    }

    public void ShowLessonMenu(GameObject menu)
    {
        targetMenu = menu;
        targetMenu.SetActive(true);
        lessonNavMenu.SetActive(false);
    }

    public void MainMenu()
    {
        targetMenu.SetActive(false);
        targetMenu = null;
        lessonNavMenu.SetActive(true);
    }

    public void GoToLesson()
    {
        if (webLessonMenu.activeSelf)
        {
            //SceneManager.LoadScene("Lesson1");
            sceneFader.FadeToScene("Lesson1");
        }
        else if (vishLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Lesson2"); 

        }
        else if (becLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Lesson3"); 
        }
        else if (authLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Lesson4"); 
        }
        else if (ssrfLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Lesson5"); 
        }
    }


    public void GoToGame()
    {
        if (webLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Level1"); 
        }
        else if (vishLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Level2"); 
        }
        else if (becLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Level3");
        }
        else if (authLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Level4"); 
        }
        else if (ssrfLessonMenu.activeSelf)
        {
            sceneFader.FadeToScene("Level5"); // Load scene for SSRF lessons
        }
    }

    public void GoToQuiz()
    {
        sceneFader.FadeToScene("Quiz"); 
    }

    public void GoToMainMenu()
    {
        sceneFader.FadeToScene("Game");
    }

    public void GoToEndless()
    {
        sceneFader.FadeToScene("Endless");
    }
}
