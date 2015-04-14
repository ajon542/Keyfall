using System.Text;

/// <summary>
/// Specifies the location with in a grid.
/// </summary>
public struct Location
{
    /// <summary>
    /// The x and y coordinates.
    /// </summary>
    public readonly int x, y;

    /// <summary>
    /// Creates a new instance of the <see cref="Location"/> struct.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public Location(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// String representation.
    /// </summary>
    /// <returns>A string representation of the location.</returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("({0}, {1})", x, y);

        return sb.ToString();
    }
}