using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
	public List<Edge> edgelist = new List<Edge>();
	public Node path = null;
	public GameObject id;
	public float xPos;
	public float yPos;
	public float zPos;
	public float f, g, h;
	public Node cameFrom;
	
	public Node(GameObject i)
	{
		id = i;
		xPos = i.transform.position.x;
		yPos = i.transform.position.y;
		zPos = i.transform.position.z;
		path = null;
	}
	
	public GameObject getId()
	{
		return id;	
	}

}
