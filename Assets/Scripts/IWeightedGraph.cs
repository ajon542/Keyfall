using System.Collections.Generic;

public interface IWeightedGraph<L>
{
    int Cost(L a, L b);
    IEnumerable<L> Neighbours(L id);
}