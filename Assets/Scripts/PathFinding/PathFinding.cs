using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    private static Vector2 mapLength;

    public static Vector2 StartNode;
    public static Vector2 EndNode;

    public static Vector2[,] offsetDirections = new Vector2[2, 6]
    {
        {
           //Even row
            new Vector2(+1, 0), new Vector2(0, -1), new Vector2(-1, -1),
            new Vector2(-1, 0), new Vector2(-1, +1), new Vector2(0, +1)
        },
        {  
            //Odd row
            new Vector2(+1, 0), new Vector2(+1, -1), new Vector2(0, -1),
            new Vector2(-1, 0), new Vector2(0, +1), new Vector2(+1, +1)
        }
    };

    public static Node FindRiverPath(Vector2 startPosition, Vector2 endPosition)
    {
        myLogger.AddToLogFile("PathFinding", "FindRiverPath Start", true);
        myLogger.AddToLogFile("PathFinding", "Details:");
        myLogger.AddToLogFile("PathFinding", "StartNodePosition: " + startPosition);
        myLogger.AddToLogFile("PathFinding", "EndNodePosition: " + endPosition);

        StartNode = startPosition;
        EndNode = endPosition;

        mapLength = MapGenerator.GetMapLength();

        List<Node> OpenNodes = new List<Node>();
        List<Node> ClosedNodes = new List<Node>();

        OpenNodes.Add(new Node(null, StartNode, 0));

        //Debug.Log(Distance(OpenNodes[0].Position, EndNode));

        int q = 0;

        myLogger.AddToLogFile("PathFinding", "FindRiverPath: Iteration start");

        while (OpenNodes.Count > 0)
        {
            q++;
            myLogger.AddToLogFile("PathFinding", q + ".Iteration, OpenNodes: " + OpenNodes.Count, true);

            Node CurrentNode = OpenNodes[0];

            //Select node with lowest fCost
            for (int i = 1; i < OpenNodes.Count; i++)
            {
                if (OpenNodes[i].fCost < CurrentNode.fCost)
                {
                    CurrentNode = OpenNodes[i];
                }
            }

            myLogger.AddToLogFile("PathFinding", "CurrentNode.Position: " + CurrentNode.Position);

            OpenNodes.Remove(CurrentNode);
            ClosedNodes.Add(CurrentNode);

            int parity = (int)(CurrentNode.Position.y % 2);

            for (int i = 0; i < offsetDirections.GetLength(1); i++)
            {
                //Neighbour is not valid
                if ((CurrentNode.Position.x == (int)mapLength.x - 1 && offsetDirections[parity, i].x > 0) || (CurrentNode.Position.x == 0 && offsetDirections[parity, i].x < 0))
                {
                    //myLogger.AddToLogFile("PathFinding", "Invalid Node: " + offsetDirections[parity, i]);
                    continue;
                }
                if ((CurrentNode.Position.y == (int)mapLength.y - 1 && offsetDirections[parity, i].y > 0) || (CurrentNode.Position.y == 0 && offsetDirections[parity, i].y < 0))
                {
                    //myLogger.AddToLogFile("PathFinding", "Invalid Node: " + offsetDirections[parity, i]);
                    continue;
                }

                //Create neighbour node
                Node neighbour = new Node(CurrentNode, CurrentNode.Position + offsetDirections[parity, i], CurrentNode.gCost);

                myLogger.AddToLogFile("PathFinding", "Neighbour Details:");
                myLogger.AddToLogFile("PathFinding", "Parent: " + CurrentNode.Position);
                myLogger.AddToLogFile("PathFinding", "Position: " + neighbour.Position);
                myLogger.AddToLogFile("PathFinding", "Offset: " + offsetDirections[parity, i]);
                myLogger.AddToLogFile("PathFinding", "Parity: " + parity);
                myLogger.AddToLogFile("PathFinding", "Neighbour Index: " + i);
                myLogger.AddToLogFile("PathFinding", "-------------");

                //path has been found
                if (neighbour.Position == EndNode)
                {
                    myLogger.AddToLogFile("PathFinding", "EndNode Found");
                    return CurrentNode;
                }

                //Neighbour in already created
                if (MapGenerator.GetMapTile(neighbour.Position) != null)
                {
                    myLogger.AddToLogFile("PathFinding", "Neighbour is already exists");
                    continue;
                }

                //already in closedNodes
                if (ClosedNodes.Contains(neighbour))
                {
                    //Debug.Log("ClosedNodes: ");
                    //Debug.Log(ClosedNodes);
                    //Debug.Log("----------");

                    //Debug.Log(CurrentNode.Position);
                    //Debug.Log(neighbour.Position);

                    myLogger.AddToLogFile("PathFinding", "Neighbour is already closed");
                    continue;
                }




                //neighbour gcost lower or neighbour not in OpenNodeList

                bool isSmaller = false;

                if (OpenNodes.Contains(neighbour))
                {
                    if (neighbour.gCost < OpenNodes.Find(x => x.Position == neighbour.Position).gCost)
                    {
                        OpenNodes.Remove(neighbour);
                        isSmaller = true;
                        myLogger.AddToLogFile("PathFinding", "new route to neighbour is shorter");
                    }
                }


                if ( (!OpenNodes.Contains(neighbour)) || (isSmaller) )
                {
                    neighbour.SetNewParent(CurrentNode, CurrentNode.gCost);
                    OpenNodes.Add(neighbour);
                    myLogger.AddToLogFile("PathFinding", "New Neighbour");
                }


                ////gCost is lower or equivalent 
                //if ( OpenNodes.Count > 0 )
                //{
                //    //Debug.Log("OpenNodesCount: " + OpenNodes.Count);
                //    //Debug.Log("Neighbour gCost: " + neighbour.gCost);
                //    //Debug.Log(OpenNodes.Find(x => x.Position == neighbour.Position));

                //    if (OpenNodes.Contains(neighbour))
                //    {
                //        if (neighbour.gCost < OpenNodes.Find(x => x.Position == neighbour.Position).gCost)
                //        {
                //            //Debug.Log("Neighbour's gCost is lower");
                //            OpenNodes.Remove(neighbour);
                //        }
                //    }                    
                //}

                ////Debug.Log("Neighbour added to OpenNodes");
                //neighbour.SetNewParent(CurrentNode, CurrentNode.gCost);
                //OpenNodes.Add(neighbour);
            }
        }
        return null;
    }

    public static Vector2 Neighbour(Vector2 hex, int direction)
    {
        int parity = (int)(hex.x % 2);
        Vector2 dir = offsetDirections[parity, direction];

        return new Vector2(hex.x + dir.x, hex.y + dir.y);
    }

    public static int Distance(Vector2 startPoint, Vector2 endPoint)
    {
        Vector3 startPointCube = OffsetToCube(startPoint);
        Vector3 endPointCube = OffsetToCube(endPoint);

        return CubeDistance(startPointCube, endPointCube);
    }

    private static int CubeDistance(Vector3 startPoint, Vector3 endPoint)
    {
        return (int)(Math.Abs(startPoint.x - endPoint.x) + Math.Abs(startPoint.y - endPoint.y) + Math.Abs(startPoint.z - endPoint.z)) / 2;
    }

    private static Vector2 CubeToOffset(Vector3 cube)
    {
        int col = (int)(cube.x + (cube.z - (cube.z % 2)) / 2);
        int row = (int)cube.z;

        return new Vector2(col, row);
    }

    private static Vector3 OffsetToCube(Vector2 hex)
    {
        int x = (int)(hex.y - (hex.x - (hex.x % 2)) / 2);
        int z = (int)hex.x;
        int y = -x - z;

        return new Vector3(x, y, z);
    }
}
