using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    [SerializeField] Slider healthbar;

    private void Awake()
    {
        healthbar.minValue = 0;
        healthbar.value = healthbar.maxValue = 100;
    }

    public void OnHealthChanged(int health)
    {
        healthbar.value = health;
    }
}
