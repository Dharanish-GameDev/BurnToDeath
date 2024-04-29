using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarinHealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;
    private Camera _camera;
    [SerializeField] private Karin _karin;

    private void Start()
    {
        _camera = Camera.main;
        slider.maxValue = _karin.KarinHP;
    }

    void Update()
    {
        if (_camera != null && !_karin.isDead)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
            slider.value = _karin.KarinHP;
        }
        if (_karin.isDead)
        {
            Destroy(this.gameObject);
        }
    }
}
