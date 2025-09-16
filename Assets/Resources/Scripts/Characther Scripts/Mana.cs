using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public Slider manaSlider;
    public Slider easeManaSlider;
    public float maxMana = 100f;
    public float mana;
    public void AddMana(float amount)
    {
        Debug.Log("Mana added");
    }
}
