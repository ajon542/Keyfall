using System.Collections.Generic;

public class Town : ILevelGenerator
{
    // House - storage of items
    // General Store - food, water, torches, medical supplies
    // Armour - legs, body, arms, helmets, boots
    // Weapons - swords, daggers, spears, axes
    // Magic / Alchemy - potions, spells, wands, teleportation devices
    // Library - books for learning about various things
    // Black market - rare items, rings, amulets

    public List<TownLayout>[,] GenerateLevel(int width, int length)
    {
        List<TownLayout>[,] townLayout = new List<TownLayout>[width, length];

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                townLayout[i, j] = new List<TownLayout>();
                townLayout[i, j].Add(TownLayout.Floor);
            }
        }

        return townLayout;
    }
}
