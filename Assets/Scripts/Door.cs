using System;
using System.Collections;

public class Door
{
    public Direction Direction { get; private set; }

    public Door()
    {
        // Generate a random direction for the door.
        System.Random rnd = new System.Random();
        int direction = rnd.Next(4);
        Direction = (Direction)direction;
    }

    public Door(Direction direction)
    {
        Direction = direction;
    }
}