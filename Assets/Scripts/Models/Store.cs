using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Store
{
    public Dictionary<string, int> Items { get; private set; }

    public Store()
    {
        Items = new Dictionary<string, int>();
        Items.Add("Sword", 100);
        Items.Add("Chainmail Armour", 100);
        Items.Add("Torch", 100);
        Items.Add("Axe", 200);
    }

    public void Buy(string item)
    {

    }
}
