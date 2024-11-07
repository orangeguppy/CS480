using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LessonNav : MonoBehaviour
{
    public GameObject lessonNavMenu;

    public GameObject webLessonMenu;
    public GameObject vishLessonMenu;
    public GameObject becLessonMenu;
    public GameObject authLessonMenu;
    public GameObject ssrfLessonMenu;

    private GameObject targetMenu;
    
    void Start()
    {
        
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
            SceneManager.LoadScene("Lesson1");
        }
        else if (vishLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Lesson2"); 

        }
        else if (becLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Lesson3"); 
        }
        else if (authLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Lesson4"); 
        }
        else if (ssrfLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Lesson5"); 
        }
    }


    public void GoToGame()
    {
        if (webLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Level1"); 
        }
        else if (vishLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Level2"); 
        }
        else if (becLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Level3");
        }
        else if (authLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Level4"); 
        }
        else if (ssrfLessonMenu.activeSelf)
        {
            SceneManager.LoadScene("Level5"); // Load scene for SSRF lessons
        }
    }

    public void GoToQuiz()
    {
        SceneManager.LoadScene("Quiz"); 
    }
}
