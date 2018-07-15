using UnityEngine;
using System.Collections;

public class Edge
{
	public Node startNode;
	public Node endNode;
	
	public Edge(Node from, Node to)
	{
		startNode = from;
		endNode = to;
	}
}
