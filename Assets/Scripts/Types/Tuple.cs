/// <summary>
/// Basic implementation of the Tuple type.
/// </summary>
/// <typeparam name="T1">The first type parameter.</typeparam>
/// <typeparam name="T2">The second type parameter.</typeparam>
public class Tuple<T1, T2>
{
    /// <summary>
    /// Gets the first parameter.
    /// </summary>
    public T1 First { get; private set; }
    
    /// <summary>
    /// Gets the second parameter.
    /// </summary>
    public T2 Second { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Tuple"/> class.
    /// </summary>
    /// <param name="first">The first parameter.</param>
    /// <param name="second">The second parameter.</param>
    internal Tuple(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
}

/// <summary>
/// Basic implementation of the Tuple type.
/// </summary>
/// <remarks>
/// This allows for type inference such as:
///     var tuple = Tuple.New(5, "hello");
/// </remarks>
public static class Tuple
{
    /// <summary>
    /// Creates a new Tuple.
    /// </summary>
    /// <typeparam name="T1">The first type parameter.</typeparam>
    /// <typeparam name="T2">The second type parameter.</typeparam>
    /// <param name="first">The first parameter.</param>
    /// <param name="second">The second parameter.</param>
    /// <returns>A new Tuple.</returns>
    public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
    {
        var tuple = new Tuple<T1, T2>(first, second);
        return tuple;
    }
}