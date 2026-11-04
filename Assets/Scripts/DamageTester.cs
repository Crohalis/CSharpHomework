using System.Collections;
using UnityEngine;
public class DamageTester : MonoBehaviour
{
    public int baseDamage;
    public int armor;
    public Transform attacker;
    public Transform target;
    public ElementType weaponElement;
    public ElementType targetElement;
    public float critChance;

    private IEnumerator Start()
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

            yield return StartCoroutine(ShowHit(finalDamage, isCritical));
        }
    }
    private IEnumerator ShowHit(int damage, bool isCritical)
    {
            Vector3 attackerStartPosition = attacker.position;
            Vector3 targetStartScale = target.localScale;

            Vector3 hitPosition = Vector3.Lerp(
                attackerStartPosition,
                target.position,
                0.65f
            );
            
            float moveDuration = 0.25f;
            
            yield return StartCoroutine(
                AttackAnimation.Move(
                    attacker,
                    attackerStartPosition,
                    hitPosition,
                    moveDuration
                )
            );

            if (damage > 0)
            {
                if (isCritical)
                {
                    target.localScale = targetStartScale * 0.6f;
                }
                else
                {
                    target.localScale = targetStartScale * 0.8f;
                }
            }

            yield return new WaitForSeconds(0.2f);

            target.localScale = targetStartScale;

            yield return StartCoroutine(
                AttackAnimation.Move(
                    attacker,
                    hitPosition,
                    attackerStartPosition,
                    moveDuration
                )
            );

            yield return new WaitForSeconds(0.5f);
        }
    }
