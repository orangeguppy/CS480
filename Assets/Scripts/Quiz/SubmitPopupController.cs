using UnityEngine;
using UnityEngine.UI;

public class SubmitPopupController : MonoBehaviour
{
    public GameObject ScoreUI;
    public Button submitButton;
    public GameObject popupPrefab;
    public void ShowUI()
    {
        popupPrefab.SetActive(true);
    }

    public void HideUI()
    {
        popupPrefab.SetActive(false);
    }

    public void showScoreUI()
    {
        ScoreUI.SetActive(true);
        HideUI();
    }

}