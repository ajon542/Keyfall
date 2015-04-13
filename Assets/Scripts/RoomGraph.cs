using System;
using System.Collections.Generic;
using System.Text;

public class RoomGraph<T> : IGraph<T>
{
    /// <summary>
    /// A simple graph data structure.
    /// </summary>
    private Dictionary<T, List<T>> graph = new Dictionary<T, List<T>>();

    /// <summary>
    /// Adds an edge (pair of vertices) to the graph.
    /// </summary>
    /// <param name="v1">The first vertex.</param>
    /// <param name="v2">The second vertex.</param>
    public void AddEdge(T v1, T v2)
    {
        if (HasEdge(v1, v2))
        {
            // Edge between v1 and v2 already exists.
            return;
        }

        // Add edge between v1 and v2.
        if (graph.ContainsKey(v1))
        {
            graph[v1].Add(v2);
        }
        else
        {
            graph[v1] = new List<T>();
            graph[v1].Add(v2);
        }

        // Add edge between v2 and v1.
        if (graph.ContainsKey(v2))
        {
            graph[v2].Add(v1);
        }
        else
        {
            graph[v2] = new List<T>();
            graph[v2].Add(v1);
        }
    }

    /// <summary>
    /// Removes an edge (pair of vertices) from the graph.
    /// </summary>
    /// <param name="v1">The first vertex.</param>
    /// <param name="v2">The second vertex.</param>
    public void RemoveEdge(T v1, T v2)
    {
        if (!graph.ContainsKey(v1))
        {
            // Since this is not a directed graph, we can make this assumption.
            return;
        }

        // Remove the vertex from the list.
        graph[v1].Remove(v2);
        graph[v2].Remove(v1);

        // If the degree of this vertex is 0 i.e. there are no incident edges.
        if (graph[v1].Count == 0)
        {
            // Null the vertex list and remove the mapping.
            graph[v1] = null;
            graph.Remove(v1);
        }

        // If the degree of this vertex is 0 i.e. there are no incident edges.
        if (graph[v2].Count == 0)
        {
            // Null the vertex list and remove the mapping.
            graph[v2] = null;
            graph.Remove(v2);
        }
    }

    /// <summary>
    /// Determines if the given vertex exists in the graph.
    /// </summary>
    /// <param name="vertex">The vertex.</param>
    /// <returns><c>true</c> if the graph contains the given vertex; otherwise, <c>false</c>.</returns>
    public bool Contains(T vertex)
    {
        return graph.ContainsKey(vertex);
    }

    /// <summary>
    /// Determines if an edge exists between the given vertices.
    /// </summary>
    /// <param name="v1">The first vertex.</param>
    /// <param name="v2">The second vertex.</param>
    /// <returns><c>true</c> if the graph contains the edge; otherwise, <c>false</c>.</returns>
    public bool HasEdge(T v1, T v2)
    {
        return graph.ContainsKey(v1) && graph[v1].Contains(v2);
    }

    /// <summary>
    /// Gets the number of vertices in this graph.
    /// </summary>
    public int VertexCount
    {
        get { return graph.Count; }
    }

    /// <summary>
    /// Gets the number of edges in this graph.
    /// </summary>
    /// <remarks>
    /// In any graph, the sum of the degrees of all vertices
    /// is equal to twice the number of edges.
    /// </remarks>
    public int EdgeCount
    {
        get
        {
            int edgeCount = 0;
            foreach (KeyValuePair<T, List<T>> edges in graph)
            {
                edgeCount += edges.Value.Count;
            }
            return edgeCount / 2;
        }
    }

    /// <summary>
    /// Gets a list of the vertices in this graph.
    /// </summary>
    public List<T> VertexList
    {
        get { return new List<T>(graph.Keys); }
    }

    /// <summary>
    /// Gets a list of the neighbouring vertices.
    /// </summary>
    /// <param name="vertex">The vertex.</param>
    /// <returns>A list of the neighbouring vertices.</returns>
    public List<T> GetNeighbours(T vertex)
    {
        if (!graph.ContainsKey(vertex))
        {
            throw new ArgumentNullException("vertex");
        }

        return new List<T>(graph[vertex]);
    }

    /// <summary>
    /// String representation of the graph.
    /// </summary>
    /// <returns>A string representation of the graph.</returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{ ");
        foreach (KeyValuePair<T, List<T>> edges in graph)
        {
            foreach (T vertex in edges.Value)
            {
                sb.AppendFormat("({0}, {1}) ", edges.Key, vertex);
            }
        }
        sb.Append("}");
        return sb.ToString();
    }
}