namespace advent_of_code_2015.Day21;
internal class Equipment
{
    public readonly string Name;
    public readonly Equipment.Type EquipmentType;
    public readonly int Damage;
    public readonly int Armor;
    public readonly int Price;

    public Equipment(
        string name,
        Equipment.Type type,
        int damage,
        int armor,
        int price)
    {
        Name = name;
        EquipmentType = type;
        Damage = damage;
        Armor = armor;
        Price = price;
    }

    public override string ToString() => Name;

    public enum Type
    {
        Weapon,
        Armor,
        Ring
    }
}
