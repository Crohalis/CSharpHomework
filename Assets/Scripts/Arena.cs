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
    private Unit m_hero;
    private Unit m_enemy;

    private void Awake()
    {
        if (heroClass == HeroClass.Warrior)
        {
            m_hero = new Unit("Warrior", 100, 10, 16);
        }
        else
        {
            m_hero = new Unit("Mage", 130, 6, 12);
        }

        int enemyIndex = Random.Range(0, 3);

        switch (enemyIndex)
        {
            case 0:
                m_enemy = new Unit("Goblin", 50, 4, 8);
                break;

            case 1:
                m_enemy = new Unit("Orc", 80, 5, 10);
                break;

            default:
                m_enemy = new Unit("Troll", 140, 8, 12);
                break;
        }
    }

    private IEnumerator Start()
    {
        heroText.text = m_enemy.name + "\nHP: " + m_enemy.health;
        enemyText.text = m_enemy.name + "\nHP: " + m_enemy.health;
        resultText.text = "";

        Debug.Log("Бой: " + m_hero.name + " против " + m_enemy.name);

        yield return new WaitForSeconds(1f);

        for (int round = 1; round <= maxRounds; round++)
        {
            int heroDamage = Random.Range(
                m_hero.minDamage,
                m_hero.maxDamage + 1
            );

            yield return StartCoroutine(
                ShowAttack(
                    heroVisual,
                    enemyVisual,
                    m_enemy,
                    enemyText,
                    heroDamage
                )
            );

            Debug.Log(
                "Раунд " + round +
                ": " + m_hero.name +
                " наносит " + heroDamage +
                " урона. " +
                m_enemy.name + " HP: " + m_enemy.health
            );

            if (m_enemy.health <= 0)
            {
                break;
            }

            int enemyDamage = Random.Range(
                m_enemy.minDamage,
                m_enemy.maxDamage + 1
            );

            yield return StartCoroutine(
                ShowAttack(
                    enemyVisual,
                    heroVisual,
                    m_hero,
                    heroText,
                    enemyDamage
                )
            );

            Debug.Log(
                m_enemy.name +
                " наносит " + enemyDamage +
                " урона. " +
                m_hero.name + " HP: " + m_hero.health
            );

            if (m_hero.health <= 0)
            {
                break;
            }
        }

        if (m_enemy.health <= 0)
        {
            resultText.text = "WIN";
        }
        else if (m_hero.health <= 0)
        {
            resultText.text = "LOSS";
        }
        else
        {
            resultText.text = "DRAW";
        }
    }
    private IEnumerator ShowAttack(
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

    
        float moveDuration = 0.25f;

        yield return StartCoroutine(
            AttackAnimation.Move(
                attackerVisual,
                startPosition,
                hitPosition,
                moveDuration
            )
        );
        
        

        targetUnit.TakeDamage(damage);

        targetText.text =
            targetUnit.name +
            "\nHP: " +
            targetUnit.health;

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(
            AttackAnimation.Move(
            attackerVisual,
            hitPosition,
            startPosition,
            moveDuration
            )
        );

        yield return new WaitForSeconds(0.5f);
    }
    
}