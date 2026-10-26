public class Unit

{
    public string Name;
    public int Health { get; private set; }
    public int MinDamage;
    public int MaxDamage;
    public Unit(string name, int health, int minDamage, int maxDamage)
    {
        Name = name;
        Health = health;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }
    public void TakeDamage(int damage)
    {
        Health = Health - damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }
}
