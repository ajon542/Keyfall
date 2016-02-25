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

    public List<string>[,] GenerateLevel(int width, int length)
    {
        List<string>[,] townLayout = new List<string>[width, length];

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                townLayout[i, j] = new List<string>();

                if(i % 2 == 0)
                {
                    townLayout[i, j].Add("Floor");
                    if(i % 4 == 0)
                    {
                        townLayout[i, j].Add("DoorEast");
                    }
                }
                else
                {
                    townLayout[i, j].Add("Grass");
                }
            }
        }

        return townLayout;
    }
}
