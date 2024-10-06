using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellManager : MonoBehaviour
{
    public GameObject SellUI;

    private PlatformManager target;

    public TextMeshProUGUI sellAmt;

    public void TargetPlatform(PlatformManager target)
    {
        this.target = target;
        Vector3 platformOffset = new Vector3(0f, 0.1f, 0f);
        transform.position = target.transform.position + platformOffset;

        sellAmt.text = "$" + target.turretBlueprint.SellAmount();
        SellUI.SetActive(true);
    }

    public void HideUI()
    {
        SellUI.SetActive(false);
    }

    public void Sell()
    {
        target.Sell();
    }
}
