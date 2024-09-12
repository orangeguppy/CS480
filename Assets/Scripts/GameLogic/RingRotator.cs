using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotator : MonoBehaviour
{
    void OnEnable()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360f, 3f).setRepeat(-1);
        LeanTween.rotateX(gameObject, -105f, 2f).setLoopPingPong();
    }
}

