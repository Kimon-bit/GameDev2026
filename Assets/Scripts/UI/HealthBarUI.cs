using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Health health;
    public Slider slider;

    void Start()
    {
        health.OnHealthChanged += UpdateBar;
    }

    void UpdateBar(float current, float max)
    {
        slider.value = current / max;
    }
}