using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Graph
    {

    public List<int>[] adjacencyList;

    public Graph()
    { 

        // Creating a graph with 15 vertices
        int V = 15;
        List<int>[] adj = new List<int>[V];

        for (int i = 0; i < V; i++)
            adj[i] = new List<int>();

 

        addEdge(adj, 0, 2);
        addEdge(adj, 0, 1);
        addEdge(adj, 1, 4); 
        addEdge(adj, 2, 3);
        addEdge(adj, 4, 7);
        addEdge(adj, 5, 6); 
        addEdge(adj, 6, 7);
        addEdge(adj, 7, 8);
        addEdge(adj, 9, 10);
        addEdge(adj, 10, 0);
        addEdge(adj, 11, 0);
        addEdge(adj, 12, 13);
        addEdge(adj, 13, 5);
        addEdge(adj, 13, 9);
        addEdge(adj, 14, 11);


        adjacencyList = adj;

    }
   
    public void addEdge(List<int>[] adj, int u, int v)
    {
        adj[u].Add(v);
    }


    public void printGraph(List<int>[] adj)
    {
        for (int i = 0; i < adj.Length; i++)
        {
            List<int> arr = adj[i];

            Debug.Log("head"+i);

            for (int k = 0; k < arr.Count; k++)
            {
                Debug.Log(arr[k]);
            }
        }
    }

 
}
