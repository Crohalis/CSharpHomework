using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public int baseDamage;
    public int armor;
    public ElementType weaponElement;
    public ElementType targetElement;
    public float critChance;

    void Start()
    {
        DamageCalculator calculator = new DamageCalculator();
        for (int i = 0; i < 3; i++)
        {
            float randomValue = Random.value;
            bool isCritical = randomValue < critChance;
            string criticalText;
            if (isCritical)
            {
                criticalText = "крит";
            }
            else
            {
                criticalText = "без крита";
            }
            string elementText;
            if (weaponElement == ElementType.None || targetElement == ElementType.None)
            {
                elementText = "без бонуса стихии";
            }
            else if (weaponElement == targetElement)
            {
                elementText = "одинаковые стихии";
            }
            else
            {
                elementText = "стихия x1.5";
            }
            int finalDamage = calculator.CalculateDamage(
                baseDamage,
                armor,
                weaponElement,
                targetElement,
                critChance,
                randomValue
            );
            Debug.Log("Удар " + (i + 1) + ": " + finalDamage + " урона, " + elementText + ", " + criticalText);
        }
    }
}