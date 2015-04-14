using System.Text;

public struct Location
{
    public readonly int x, y;

    public Location(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("({0}, {1})", x, y);

        return sb.ToString();
    }
}