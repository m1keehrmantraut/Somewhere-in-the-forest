using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
    }
}
