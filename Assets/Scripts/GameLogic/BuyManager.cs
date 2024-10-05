using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public GameObject buyUI;

    private PlatformManager target;

    public void TargetPlatform(PlatformManager target)
    {
        this.target = target;
        Vector3 platformOffset = new Vector3(0f, 0.3f, 0f);
        transform.position = target.transform.position + platformOffset;

        buyUI.SetActive(true);
    }

    public void HideUI()
    {
        buyUI.SetActive(false);
    }
}
