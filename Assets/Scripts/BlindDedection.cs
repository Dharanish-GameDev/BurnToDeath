using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindDedection : MonoBehaviour
{
    public static bool BlindIsActive = false;
    private void OnEnable()
    {
        BlindIsActive = true;
    }
    private void OnDisable()
    {
        BlindIsActive = false;
    }
}