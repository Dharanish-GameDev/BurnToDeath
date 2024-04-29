using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boar_healthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;
    private Camera _camera;
    [SerializeField] private WildBoar _wildBoar;

    private void Start()
    {
        _camera = Camera.main;
        slider.maxValue = _wildBoar.boarHp;
    }

    void Update()
    {
        if (_camera != null && !_wildBoar.isDead)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
            slider.value = _wildBoar.boarHp;
        }
        if(_wildBoar.isDead)
        {
            Destroy(this.gameObject);
        }
    }

   
}
