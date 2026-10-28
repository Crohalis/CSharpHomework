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

    IEnumerator Start()
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
    IEnumerator ShowHit(int damage, bool isCritical)
    {
            Vector3 attackerStartPosition = attacker.position;
            Vector3 targetStartScale = target.localScale;

            Vector3 hitPosition = Vector3.Lerp(
                attackerStartPosition,
                target.position,
                0.65f
            );

            float timer = 0f;
            float moveDuration = 0.25f;

            while (timer < moveDuration)
            {
                timer += Time.deltaTime;

                attacker.position = Vector3.Lerp(
                    attackerStartPosition,
                    hitPosition,
                    timer / moveDuration
                );

                yield return null;
            }

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

            timer = 0f;

            while (timer < moveDuration)
            {
                timer += Time.deltaTime;

                attacker.position = Vector3.Lerp(
                    hitPosition,
                    attackerStartPosition,
                    timer / moveDuration
                );

                yield return null;
            }

            attacker.position = attackerStartPosition;

            yield return new WaitForSeconds(0.5f);
        }
    }
