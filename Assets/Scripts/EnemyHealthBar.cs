using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Color lowClr;
    public Color highColor;
    public Vector3 offset;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (_camera != null)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        }
    }

    public void EnemyHeathBar(float currentHealth, float maxHealth)
    {
        slider.gameObject.SetActive(currentHealth < maxHealth);
        slider.value = currentHealth;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowClr, highColor, slider.normalizedValue);
    }
}
  
