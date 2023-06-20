using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }
}
