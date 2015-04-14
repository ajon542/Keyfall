using System.Collections.Generic;

public interface IWeightedGraph<L>
{
    int Cost(L id);
    IEnumerable<L> Neighbours(L id);
}