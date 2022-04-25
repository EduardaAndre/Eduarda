using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Link // Foi definido dois pontos onde vai percorrer  
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}
public class WpManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();
    //Link seria as coisas que ele vai pecorrer(foi criado o grafo "percusso" a partir dos waypoints e links)
    void Start()
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            foreach (Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }
    // Mostra o percusso na tela, as movimentações 
    void Update()
    {
        graph.debugDraw();
    }
}

