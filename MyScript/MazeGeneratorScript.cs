using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorScript : MonoBehaviour
{

	[SerializeField] GameObject square;
	bool[,] field;
	int size;
	[SerializeField] Collider boxCollider;
	Vector3 boxSize;
	[SerializeField] Transform spawnMazeHere;
	public static Vector3 spawnVector;


	public class Node
	{

		public int zDistance;
		public int xDistance;
		public int z;
		public int x;
		public Node leftNode;
		public Node rightNode;
		public Vector2 center;

		public Node()
		{

		}

		public Node(int x, int z, int xDistance, int zDistance)
		{
			this.x = x;
			this.z = z;
			this.zDistance = zDistance;
			this.xDistance = xDistance;
			center = GetCenter();

			this.leftNode = null;
			this.rightNode = null;
		}

		public Vector2 GetCenter()
		{
			int xMiddle = (this.xDistance / 2) + x;
			int zMiddle = (this.zDistance / 2) + z;
			return new Vector2(xMiddle, zMiddle);
		}
		public void Centering()
		{
			int xMiddle = (this.xDistance / 2) + x;
			int zMiddle = (this.zDistance / 2) + z;
			this.center = new Vector2(xMiddle, zMiddle);
		}
	}

	// Start is called before the first frame update

	public void RandomGenerator(Node parentNode, int iteration)
	{
		//		System.out.println("Parent : " + parentNode.x + "," + parentNode.z + " Distance X :" + parentNode.xDistance + " DistanceZ : " + parentNode.zDistance);
		if (iteration <= 0)
			return;

		int getDirection = Random.RandomRange(0, 2);
		Node leftChild = new Node(parentNode.x, parentNode.z, parentNode.xDistance, parentNode.zDistance);
		Node rightChild = new Node(parentNode.x, parentNode.z, parentNode.xDistance, parentNode.zDistance);

		if (getDirection == 0)
		{

            leftChild.xDistance = (int) Random.RandomRange(1.5f, leftChild.xDistance - 1f);

			rightChild.x += leftChild.xDistance;
			rightChild.xDistance -= leftChild.xDistance;
		}
		else 
		{
            //float randomRange = Random.RandomRange(1.5f, 3f);
            leftChild.zDistance = (int)Random.RandomRange(1.5f, leftChild.zDistance - 1f);

			rightChild.z += leftChild.zDistance;
			rightChild.zDistance -= leftChild.zDistance;
		}

		leftChild.Centering();
		rightChild.Centering();

		parentNode.leftNode = leftChild;
		parentNode.rightNode = rightChild;

		//		System.out.println("Iteration : " + iteration);
		//		System.out.println("Left Child  : " + (int) leftChild.x + "," + (int) leftChild.z + "|  DistanceX : " + (int) leftChild.xDistance + " DistanceZ : " +(int) leftChild.zDistance );
		//		System.out.println("Right Child : " + (int) rightChild.x + "," + (int) rightChild.z + "|  DistanceX : " + (int) rightChild.xDistance + " DistanceZ : " +(int) rightChild.zDistance );

		RandomGenerator(leftChild, iteration - 1);
		RandomGenerator(rightChild, iteration - 1);
	}
	public Node GetMostLeft(Node parent)
    {
		if(parent.leftNode == null)
        {
			return parent;
        }
        else
        {
			return GetMostLeft(parent.leftNode);
        }
    }

	public Node GetMostRight(Node parent)
	{
		if (parent.rightNode == null)
		{
			float tempX = (parent.x * boxSize.x) + spawnMazeHere.position.x;
			float tempZ = (parent.z * boxSize.z) + spawnMazeHere.position.z;
			//Debug.Log("Right : " + tempX + " " + tempZ);

			return parent;
		}
		else
		{
			return GetMostRight(parent.rightNode);
		}
	}
	public void MakePath(Node parent)
	{
		if (parent.leftNode == null || parent.rightNode == null)
		{
			//Debug.Log("WOW");
			return;
		}
        //Debug.Log("Left Node : " + parent.leftNode.center.x + " " + parent.leftNode.center.y);
        //Debug.Log("Right Node : " + parent.rightNode.center.x + " " + parent.rightNode.center.y);

        for (int i = (int) parent.leftNode.center.x; i <= parent.rightNode.center.x; i++)
		{
			for (int j = (int) parent.leftNode.center.y; j <= parent.rightNode.center.y; j++)
            {
                //Debug.Log(i + " " + j);
                
                    field[i, j] = true;
                

            }
        }
		MakePath(parent.leftNode);
		MakePath(parent.rightNode);
	}




	public void MakeMaze()
    {
      for(int i = 0;i < size; i++)
        {
           for (int j = 0; j < size; j++)
            {
                if(!field[i,j])
                {
					float tempX = (i * boxSize.x) + spawnMazeHere.position.x;
					float tempZ = (j * boxSize.z) + spawnMazeHere.position.z;

					Vector3 coor = new Vector3(tempX, boxSize.y *1.6f, tempZ);
                    Instantiate(square, coor, Quaternion.identity);
                }
            }
        }
    }

	static public float maxRatio;

    void Start()
    {
		maxRatio = 0.3f;
		size = 50;
		boxSize = boxCollider.bounds.size;
		//Debug.Log("box collide : " + boxCollider.bounds.size.y);
		//Debug.Log(boxSize.x + " " + boxSize.y + " " + boxSize.z);
        field = new bool[size * 2, size * 2];
        Node root = new Node(0,0, size, size);
        RandomGenerator(root, 8);
        MakePath(root);
		MakeGetOut(root);

		Node mostLeftChild = GetMostLeft(root);
		spawnVector.x = spawnMazeHere.position.x + (mostLeftChild.GetCenter().x * boxSize.x);
		spawnVector.z = spawnMazeHere.position.z + (mostLeftChild.GetCenter().y * boxSize.z);
		spawnVector.y = Terrain.activeTerrain.SampleHeight(new Vector3(spawnVector.x, Mathf.Infinity, spawnVector.z));

        MakeMaze();
    }

	public void MakeGetOut(Node root)
    {

		Node mostRightChild = GetMostRight(root);

        for (int i = (int)mostRightChild.x; i < size; i++)
        {
            field[(int)mostRightChild.z, i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
