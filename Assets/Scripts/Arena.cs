using UnityEngine;

public class Arena : MonoBehaviour
{
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

    void Start()
    {
        Debug.Log("Бой: " + hero.Name + " против " + enemy.Name);
        for (int round = 1; round <= maxRounds; round++)
        {
            int heroDamage = Random.Range(hero.MinDamage, hero.MaxDamage + 1);
            enemy.TakeDamage(heroDamage);

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

            int enemyDamage = Random.Range(enemy.MinDamage, enemy.MaxDamage + 1);

            hero.TakeDamage(enemyDamage);

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
            Debug.Log("Результат: Win");
        }
        else if (hero.Health <= 0)
        {
            Debug.Log("Результат: Loss");
        }
        else
        {
            Debug.Log("Результат: Draw");
        }
    }
}