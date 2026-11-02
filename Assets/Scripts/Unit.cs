public class Unit

{
    public string name;
    public int health { get; private set; }
    public int minDamage;
    public int maxDamage;
    public Unit(string unitName, int unitHealth, int unitMinDamage, int unitMaxDamage)
    {
        name = unitName;
        health = unitHealth;
        minDamage =unitMinDamage;
        maxDamage = unitMaxDamage;
    }
    public void TakeDamage(int damage)
    {
        health = health - damage;
        if (health < 0)
        {
            health = 0;
        }
    }
}
