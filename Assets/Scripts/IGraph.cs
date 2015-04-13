using System.Collections.Generic;

public interface IGraph<T>
{
    List<T> VertexList { get; }
    int EdgeCount { get; }
    int VertexCount { get; }

    void AddEdge(T v1, T v2);
    void RemoveEdge(T v1, T v2);
    bool Contains(T vertex);
    bool HasEdge(T v1, T v2);
    List<T> GetNeighbours(T vertex);
}
