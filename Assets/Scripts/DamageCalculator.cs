using System;
public class DamageCalculator
{
    public int CalculateDamage(int baseDamage, int armor, ElementType weaponElement, ElementType targetElement, float critChance, float randomValue)
    {
        int currentDamage = baseDamage - armor;
        
        if (currentDamage < 1)
        {
            currentDamage = 1;
        }
        if (weaponElement == targetElement && weaponElement != ElementType.None)
        {
            currentDamage = 0;
        }

        if (
            (weaponElement == ElementType.Fire && targetElement == ElementType.Ice)
            ||
            (weaponElement == ElementType.Ice && targetElement == ElementType.Fire)
        )
        {
            currentDamage = (int)Math.Floor(currentDamage * 1.5);
        }

        if (randomValue < critChance)
        {
            currentDamage = currentDamage * 2;
        }
        return currentDamage;
        
    }
}
