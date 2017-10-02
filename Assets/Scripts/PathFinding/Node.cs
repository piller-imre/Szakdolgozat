using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public Vector2 Position { get; private set; }
    public int gCost { get; private set; }
    public int hCost { get; private set; }
    public int fCost { get; private set; }
    public Node Parent { get; private set; }

    public Node(Node parent, Vector2 position, int parentGCost)
    {
        Parent = parent;
        Position = position;
        gCost = parentGCost + 1;

        CalculateCosts();
    }

    public void SetNewParent(Node parent, int parentGCost)
    {
        Parent = parent;
        gCost = parentGCost + 1;
        CalculateCosts();
    }

    public void CalculateCosts()
    {
        Vector2 startNode = PathFinding.StartNode;
        Vector2 endNode = PathFinding.EndNode;

        hCost = PathFinding.Distance(endNode, Position);
        fCost = gCost + hCost;
    }

    public override bool Equals(object other)
    {
        bool isEqual = false;
        Node otherNode = other as Node;

        if (this.Position == otherNode.Position)
        {
            isEqual = true;
        }

        return isEqual;
    }

    public override string ToString()
    {
        return "Node: Position: " + Position + ", gCost: " + gCost + ", hCost: " + hCost + ", fCost: " + fCost + ", ParentPosition: " + Parent.Position;
    }
}
