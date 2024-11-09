using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarRotator : MonoBehaviour
{
    void OnEnable()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360f, 2f).setRepeat(-1);
    }
}
