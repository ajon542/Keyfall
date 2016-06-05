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

    private List<string>[,] townLayout;

    public List<string>[,] GenerateLevel(int width, int length)
    {
        townLayout = new List<string>[width, length];

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                townLayout[i, j] = new List<string>();
                townLayout[i, j].Add("Grass");
            }
        }

        return townLayout;
    }

    private void GenerateRoom(Location location, int width, int length)
    {
        for (int i = location.x; i < location.x + width; ++i)
        {
            for (int j = location.y; j < location.y + length; ++j)
            {
                if (j == location.y)
                {
                    townLayout[i, j].Add("WallSouth");
                }
                if (i == location.x)
                {
                    townLayout[i, j].Add("WallWest");
                }
                if (i == location.x + width - 1)
                {
                    townLayout[i + 1, j].Add("WallWest");
                }
                if (j == location.y + length - 1)
                {
                    townLayout[i, j + 1].Add("WallSouth");
                }
            }
        }
    }
}
