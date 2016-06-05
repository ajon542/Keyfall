using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class represents a simple item of value.
/// </summary>
/// <remarks>
/// At some point we may want to revisit this design as there is going
/// to be many types of items, for example, food, armour, weapon, quest etc.
/// Once we start to get into this detail, a description of the item is
/// not going to be enough.
/// </remarks>
public class Item
{
    public string Name { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }
}