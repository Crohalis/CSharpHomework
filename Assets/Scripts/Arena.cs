using System.Collections;
using TMPro;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public Transform heroVisual;
    public Transform enemyVisual;

    public TMP_Text heroText;
    public TMP_Text enemyText;
    public TMP_Text resultText;
    public HeroClass heroClass;
    public int maxRounds = 10;
    private Unit hero;
    private Unit enemy;

    void Awake()
    {
        if (heroClass == HeroClass.Warrior)
        {
            hero = new Unit("Warrior", 100, 10, 16);
        }
        else
        {
            hero = new Unit("Mage", 130, 6, 12);
        }

        int enemyIndex = Random.Range(0, 3);

        switch (enemyIndex)
        {
            case 0:
                enemy = new Unit("Goblin", 50, 4, 8);
                break;

            case 1:
                enemy = new Unit("Orc", 80, 5, 10);
                break;

            case 2:
                enemy = new Unit("Troll", 140, 8, 12);
                break;
        }
    }

    IEnumerator Start()
    {
        heroText.text = hero.Name + "\nHP: " + hero.Health;
        enemyText.text = enemy.Name + "\nHP: " + enemy.Health;
        resultText.text = "";

        Debug.Log("Бой: " + hero.Name + " против " + enemy.Name);

        yield return new WaitForSeconds(1f);

        for (int round = 1; round <= maxRounds; round++)
        {
            int heroDamage = Random.Range(
                hero.MinDamage,
                hero.MaxDamage + 1
            );

            yield return StartCoroutine(
                ShowAttack(
                    heroVisual,
                    enemyVisual,
                    enemy,
                    enemyText,
                    heroDamage
                )
            );

            Debug.Log(
                "Раунд " + round +
                ": " + hero.Name +
                " наносит " + heroDamage +
                " урона. " +
                enemy.Name + " HP: " + enemy.Health
            );

            if (enemy.Health <= 0)
            {
                break;
            }

            int enemyDamage = Random.Range(
                enemy.MinDamage,
                enemy.MaxDamage + 1
            );

            yield return StartCoroutine(
                ShowAttack(
                    enemyVisual,
                    heroVisual,
                    hero,
                    heroText,
                    enemyDamage
                )
            );

            Debug.Log(
                enemy.Name +
                " наносит " + enemyDamage +
                " урона. " +
                hero.Name + " HP: " + hero.Health
            );

            if (hero.Health <= 0)
            {
                break;
            }
        }

        if (enemy.Health <= 0)
        {
            resultText.text = "WIN";
        }
        else if (hero.Health <= 0)
        {
            resultText.text = "LOSS";
        }
        else
        {
            resultText.text = "DRAW";
        }
    }
    IEnumerator ShowAttack(
        Transform attackerVisual,
        Transform targetVisual,
        Unit targetUnit,
        TMP_Text targetText,
        int damage)
    {
        Vector3 startPosition = attackerVisual.position;

        Vector3 hitPosition = Vector3.Lerp(
            startPosition,
            targetVisual.position,
            0.65f
        );

        float timer = 0f;
        float moveDuration = 0.25f;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;

            attackerVisual.position = Vector3.Lerp(
                startPosition,
                hitPosition,
                timer / moveDuration
            );

            yield return null;
        }

        targetUnit.TakeDamage(damage);

        targetText.text =
            targetUnit.Name +
            "\nHP: " +
            targetUnit.Health;

        yield return new WaitForSeconds(0.2f);

        timer = 0f;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;

            attackerVisual.position = Vector3.Lerp(
                hitPosition,
                startPosition,
                timer / moveDuration
            );

            yield return null;
        }

        attackerVisual.position = startPosition;

        yield return new WaitForSeconds(0.5f);
    }
}