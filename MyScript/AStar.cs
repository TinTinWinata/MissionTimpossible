
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AStar : MonoBehaviour
{

    static float gridSize = 1f;
    int[] tempX = {1, 0 , -1, 0, 1, 1,-1, -1};
    int[] tempZ = { 1, 0, -1, 0, 1, 1, -1, -1};

    int lastX;
    int lastZ;
    public Transform target1, target2;
        public List<Vector2> SearchAStar(Transform target1, Transform target2)
    {
        this.target1 = target1;
        this.target2 = target2;

        List<Vector2> pathFinded = Pathfind((int)target1.position.x, (int)target1.position.z, (int)target2.position.x, (int)target2.position.z);
        pathFinded.Reverse();
        return pathFinded;
    }



    public float GetHeuristic(float x, float z)
    {
        float targetX = target2.position.x;
        float targetZ = target2.position.z;

        x += (gridSize / 2);
        z += (gridSize / 2);

        float value = Mathf.Sqrt(Mathf.Pow((x - targetX), 2) + Mathf.Pow((z - targetZ), 2));
        return value;
    }

    public class Node
    {
        public int nextX;
        public int nextZ;
        public int prevX;
        public int prevZ;
        public int x;
        public int z;
        public bool visited;
        public float heuristicValue;
        Transform target;
        public Node(int x, int z, Transform target)
        {
            this.target = target;
            this.x = x;
            this.z = z;
            this.visited = false;
            this.heuristicValue = GetHeuristic(x, z);
            this.prevX = -99;
            this.prevZ = -99;

        }
        public float GetHeuristic(float x, float z)
        {
            float targetX = target.position.x;
            float targetZ = target.position.z;

            x += (gridSize / 2);
            z += (gridSize / 2);

            float value = Mathf.Sqrt(Mathf.Pow((x - targetX), 2) + Mathf.Pow((z - targetZ), 2));
            return value;
        }
    }
        Node[,] nodeList = new Node[9999,9999];

    private Node GetNode(int x, int z)
    {
        if (nodeList[x,z] == null)
        {
            nodeList[x, z] = new Node(x, z, target2);
            return nodeList[x, z];
        }
        else
        {
            return nodeList[x, z];
        }
    }

    public bool IsValid(int x, int z, int prevX, int prevZ)
    {
        bool isAreaValid = CalculateAreaScript.validatedArea[x, z];
        bool isNotVisited = GetNode(x, z).visited == false;
        bool isNotExceedMap = x >= 0 && z >= 0 && x <= 1000 & z <= 1000;

        //Debug.Log(x+" "+z+" "+isAreaValid + " " + isNotVisited + " " + isNotExceedMap);

        if(isAreaValid && isNotVisited && isNotExceedMap)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public float GetDistance(float x, float z, float targetX, float targetZ)
    {
        float distance = Mathf.Sqrt((Mathf.Pow((x - targetX), 2f) + Mathf.Pow((z - targetZ), 2f)));
        return distance;
    }

    public bool CheckDiagonal(int i, int j)
    {
        if (i != 0 && j != 0)
        {
            return true;
        } else
        { 
        return false;
        }
    }
    public List<Vector2> Pathfind(int x, int z, int targetX, int targetZ)
    {
        List<Vector2> pathList = new List<Vector2>();
        bool foundTarget = false;

        GetNode(x, z).visited = true;
        List<Node> nodeQueue = new List<Node>();
        nodeQueue.Add(new Node(x, z, target2));

 

        while(nodeQueue.Count > 0 && !foundTarget)
        {
            float searchHeuristic = Mathf.Infinity;
            int idxLowestHeuristic = -1;
            for(int i =0;i < nodeQueue.Count;i++)
            {
                if(nodeQueue[i].heuristicValue < searchHeuristic)
                {
                    searchHeuristic = nodeQueue[i].heuristicValue;
                    idxLowestHeuristic = i;
                }
            }

            Node nodeCheck = nodeQueue[idxLowestHeuristic];
            nodeQueue.RemoveAt(idxLowestHeuristic);
            //Debug.Log("Checking : " + nodeCheck.x + " " + nodeCheck.z);

            if (GetDistance(nodeCheck.x, nodeCheck.z, targetX, targetZ) <= 5)
            {
                //Debug.Log("KETEMU");
                foundTarget = true;
                lastX = nodeCheck.x;
                lastZ = nodeCheck.z;
                break;
            }

            foreach(int i in tempX)
            {
                foreach (int j in tempZ)
                {
                    if (i == 0 && j == 0)
                        continue;
                    int nextX = nodeCheck.x + i;
                    int nextZ = nodeCheck.z + j;

                    if (IsValid(nextX, nextZ, nodeCheck.x, nodeCheck.z))
                    {
                        if (CheckDiagonal(i, j))
                        {
                            if (!IsValid(nextX - i, nextZ, nodeCheck.x, nodeCheck.z) && IsValid(nextX, nextZ - j, nodeCheck.x, nodeCheck.z))
                                continue;
                        }
                        GetNode(nextX, nextZ).visited = true;
                        GetNode(nextX, nextZ).prevX = nodeCheck.x;
                        GetNode(nextX, nextZ).prevZ = nodeCheck.z;
                        Node nextNode = new Node(nextX, nextZ, target2);
                        //Debug.Log("Next node is : " + nextNode.x + " | " + nextNode.z);
                        nodeQueue.Add(nextNode);
                    }
                    else
                    {
                        //Debug.Log("Not Valid");
                    }
                }
            }


        }

        if (foundTarget)
        {
            int xCurr = lastX;
            int zCurr = lastZ;
            //Debug.Log("FOUNDED" + lastX + " | " + lastZ);

            while (GetNode(xCurr, zCurr).prevX > 0)
            {
                int tempX = xCurr;
                xCurr = GetNode(xCurr, zCurr).prevX;
                zCurr = GetNode(tempX, zCurr).prevZ;

                Vector2 curr = new Vector2(xCurr, zCurr);
                //Debug.Log("Founded :" + xCurr + " | " + zCurr);
                pathList.Add(curr);
            }
        }
        else
        {
            //Debug.Log("Not found");
        }

        return pathList;
    }
   


    }


