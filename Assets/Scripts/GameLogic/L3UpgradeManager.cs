using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L3UpgradeManager : MonoBehaviour
{
    public GameObject finalUpgradeUI;

    private PlatformManager target;

    [Header("Buttons")]
    public Button upgradeButton;
    public Button upgrade2Button;
    public Button sellButton;


    [Header("Faded Button Settings")]
    public float fadeAlpha = 0.5f;


    public void TargetPlatform(PlatformManager target)
    {
        this.target = target;
        Vector3 platformOffset = new Vector3(0f, 0.1f, 0f);
        transform.position = target.transform.position + platformOffset;

        finalUpgradeUI.SetActive(true);
    }

    public void HideUI()
    {
        finalUpgradeUI.SetActive(false);
    }

    public void UpgradeA()
    {
        target.Stage3AUpgrade();
        Debug.Log("upgrad1e");
    }

    public void UpgradeB()
    {
        target.Stage3BUpgrade();
        Debug.Log("upgrad1e");
    }

        public void Sell()
    {
        target.Sell();
    }
}
