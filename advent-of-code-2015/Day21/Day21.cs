namespace advent_of_code_2015.Day21;
internal class Day21 : AdventSolution
{
    protected override long part1ExampleExpected => 65;
    protected override long part1InputExpected => 111;
    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }

    private long work(string[] input)
    {
        var shopText = getShopText();
        var weapons = getWeapons(shopText);
        var armors = getArmors(shopText);
        var rings = getRings(shopText);

        var boss = getBossStats(input);
        var you = getYourStats(boss);

        return leastAmountOfGold(you, boss, weapons, armors, rings);
    }

    private long leastAmountOfGold(
        Creature you,
        Creature boss,
        IList<Equipment> weapons,
        IList<Equipment> armors,
        IList<Equipment> rings)
    {
        var leastAmountOfGold = long.MaxValue;

        foreach (var weapon in weapons)
        {
            foreach (var armor in armors)
            {
                foreach (var ring in rings)
                {
                    foreach (var otherRing in rings.Where(x => x != ring))
                    {
                        var goldUsed = getGoldUsed(weapon, armor, ring, otherRing);

                        if (goldUsed < leastAmountOfGold)
                        {
                            var won = fight(you, boss, weapon, armor, ring, otherRing);

                            if (won)
                            {
                                leastAmountOfGold = Math.Min(leastAmountOfGold, goldUsed);
                            }
                        }
                    }
                }
            }
        }

        return leastAmountOfGold;
    }

    private bool fight(
        Creature you,
        Creature boss,
        Equipment weapon,
        Equipment armor,
        Equipment ring1,
        Equipment ring2)
    {
        bool playerTurn = true;
        you.Reset();
        boss.Reset();

        var playerDamage = you.Damage + weapon.Damage + armor.Damage + ring1.Damage + ring2.Damage;
        var playerArmor = you.Armor + weapon.Armor + armor.Armor + ring1.Armor + ring2.Armor;

        while (you.IsAlive() && boss.IsAlive())
        {
            if (playerTurn)
            {
                boss.ReceiveAttack(
                    playerDamage,
                    boss.Armor);
            }

            else
            {
                you.ReceiveAttack(
                    boss.Damage,
                    playerArmor);
            }

            playerTurn = !playerTurn;
        }

        return you.IsAlive();
    }

    private long getGoldUsed(params Equipment[] items) =>
        items.Sum(item => item.Price);

    private Creature getYourStats(Creature boss)
    {
        if (boss.HitPoints == 12)
        {
            return getExampleYou();
        }

        else
        {
            return getInputYou();
        }
    }

    private Creature getExampleYou() => new Creature(8, 0, 0);

    private Creature getInputYou() => new Creature(100, 0, 0);

    private Creature getBossStats(IList<string> input) =>
        new Creature(
            statFromLine(input.Skip(0).First()),
            statFromLine(input.Skip(1).First()),
            statFromLine(input.Skip(2).First())
        );

    private int statFromLine(string line) => int.Parse(line.Split(": ")[1]);

    private IList<string> getShopText() =>
        File.ReadAllLines(@"Day21\shop.txt");

    private IList<Equipment> getWeapons(IList<string> shopText) => getEquipments(shopText, Equipment.Type.Weapon);

    private IList<Equipment> getArmors(IList<string> shopText) => getEquipments(shopText, Equipment.Type.Armor);

    private IList<Equipment> getRings(IList<string> shopText) => getEquipments(shopText, Equipment.Type.Ring);

    private IList<Equipment> getEquipments(
        IList<string> shopText,
        Equipment.Type type)
    {
        int index = 0;

        while (!shopText[index].StartsWith(type.ToString()))
        {
            index += 1;
        }

        // skip header
        index += 1;

        var list = new List<Equipment>();

        while (index < shopText.Count && shopText[index] != String.Empty)
        {
            var split = shopText[index].Split("  ", StringSplitOptions.RemoveEmptyEntries);
            list.Add(
                new Equipment(
                    split[0],
                    type,
                    int.Parse(split[2]),
                    int.Parse(split[3]),
                    int.Parse(split[1])
                )
            );
            index += 1;
        }

        return list;
    }

    protected override long part1Work(string[] input) => work(input);

    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }
}
