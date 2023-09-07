namespace advent_of_code_2015.Day21;
internal class Creature
{
    public int HitPoints { get; protected set; }
    private readonly int startingHitPoints;

    public readonly int Damage;
    public readonly int Armor;

    public Creature(
        int hitPoints,
        int damage,
        int armor)
    {
        HitPoints = hitPoints;
        startingHitPoints = hitPoints;
        Damage = damage;
        Armor = armor;
    }

    public void ReceiveAttack(
        int incomingDamage,
        int armorStat)
    {
        var amount = incomingDamage - armorStat;
        var atLeastOne = Math.Max(1, amount);

        HitPoints -= atLeastOne;
    }

    public void Reset()
    {
        HitPoints = startingHitPoints;
    }

    public bool IsAlive() => HitPoints > 0;
}
