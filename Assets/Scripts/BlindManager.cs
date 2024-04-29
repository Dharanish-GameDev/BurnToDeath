using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindManager : MonoBehaviour
{
    public static GameObject LftblindSpin;
    public static GameObject RgtblindSpin;
    public static GameObject blindPanel;
    public static GameObject blindDedect;
    [SerializeField] private GameObject _lftBlindSpin;
    [SerializeField] private GameObject _rgtBlindSpin;
    [SerializeField] private GameObject _blindPanel;
    [SerializeField] private GameObject _blindDedect;
    public static bool canCheck = false;
    private void Start()
    {
        LftblindSpin = _lftBlindSpin;
        RgtblindSpin = _rgtBlindSpin;
        blindPanel = _blindPanel;
        blindDedect = _blindDedect;
    }
}
