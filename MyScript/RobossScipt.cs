using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RobossScipt : MonoBehaviour
{
    static float gridSize = 1f;
    int[] tempX = { 1, 0, -1, 0,};
    int[] tempZ = { 1, 0, -1, 0,};

    int lastX;
    int lastZ;
    public Transform target1, target2;
    float targetX;
    float targetZ;
    Node[,] nodeList;


    public List<Vector2> SearchAStar(Transform target1, float targetX, float targetZ)
    {
        this.targetX = targetX;
        this.targetZ = targetZ;

        List<Vector2> pathFinded = Pathfind((int)target1.position.x, (int)target1.position.z, (int)targetX, (int)targetZ);
        Debug.Log("Count : " + pathFinded.Count);
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
        public Node(int x, int z, float targetX, float targetZ)
        {
            this.target = target;
            this.x = x;
            this.z = z;
            this.visited = false;
            this.heuristicValue = GetHeuristic(x, z, targetX, targetZ);
            this.prevX = -99;
            this.prevZ = -99;

        }

        public float GetHeuristic(float x, float z, float targetX, float targetZ)
        {
            x += (gridSize / 2);
            z += (gridSize / 2);

            float value = Mathf.Sqrt(Mathf.Pow((x - targetX), 2) + Mathf.Pow((z - targetZ), 2));
            return value;
        }
    }

    private Node GetNode(int x, int z)
    {
        if (nodeList[x, z] == null)
        {
            nodeList[x, z] = new Node(x, z, targetX, targetZ);
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

        //Debug.Log(x + " " + z + " " + isAreaValid + " " + isNotVisited + " " + isNotExceedMap);

        if (isAreaValid && isNotVisited && isNotExceedMap)
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
        }
        else
        {
            return false;
        }
    }
    public List<Vector2> Pathfind(int x, int z, int targetX, int targetZ)
    {
        nodeList = new Node[1000, 1000];
        List<Vector2> pathList = new List<Vector2>();
        bool foundTarget = false;

        GetNode(x, z).visited = true;
        List<Node> nodeQueue = new List<Node>();
        nodeQueue.Add(new Node(x, z, targetX, targetZ));
          
        while (nodeQueue.Count > 0 && !foundTarget)
        {
            //Debug.Log("Node Queue Count : " + nodeQueue.Count);
            float searchHeuristic = Mathf.Infinity;
            int idxLowestHeuristic = -1;
            for (int i = 0; i < nodeQueue.Count; i++)
            {
                if (nodeQueue[i].heuristicValue < searchHeuristic)
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
                Debug.Log("KETEMU");
                foundTarget = true;
                lastX = nodeCheck.x;
                lastZ = nodeCheck.z;
                break;
            }

            foreach (int i in tempX)
            {
                foreach (int j in tempZ)
                {
                    if (i == 0 && j == 0)
                        continue;
                    int nextX = nodeCheck.x + i;
                    int nextZ = nodeCheck.z + j;
                    //Debug.Log("Next X : " + nextX);
                    //Debug.Log("Next Z : " + nextZ);

                    if (IsValid(nextX, nextZ, nodeCheck.x, nodeCheck.z))
                    {
                        GetNode(nextX, nextZ).visited = true;
                        GetNode(nextX, nextZ).prevX = nodeCheck.x;
                        GetNode(nextX, nextZ).prevZ = nodeCheck.z;
                        Node nextNode = new Node(nextX, nextZ, targetX, targetZ);
                        nodeQueue.Add(nextNode);
                    }
                    else
                    {
                        //Debug.Log("Not Valid");
                    }
                }
            }
            // Round Checking


        }

        if (foundTarget)
        {
            int xCurr = lastX;
            int zCurr = lastZ;
            Debug.Log("FOUNDED" + lastX + " | " + lastZ);

            while (GetNode(xCurr, zCurr).prevX > 0)
            {
                int tempX = xCurr;
                xCurr = GetNode(xCurr, zCurr).prevX;
                zCurr = GetNode(tempX, zCurr).prevZ;

                Vector2 curr = new Vector2(xCurr, zCurr);
                Debug.Log("Founded :" + xCurr + " | " + zCurr);
                pathList.Add(curr);
            }
        }
        else
        {
            //Debug.Log("Not found");
        }

        return pathList;
    }



    public int maxHealth = 2000;
    private int health;
    private int damage;
    public int bulletSpeed = 2000;
    public float fireRate = 0.07f;

    public float fireRateTimer;
    private Animator anim;
    public static bool bossStart;

    public static bool isShooting;
    public static bool firingState;

    [SerializeField] Slider healthSlider;
    [SerializeField] Transform spawnBullet;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform mainCharacter;

    [SerializeField] Transform corner1;
    [SerializeField] Transform corner2;
    [SerializeField] Transform corner3;
    [SerializeField] Transform corner4;

    [Header("Astar")]
    [SerializeField] float chaseSpeed = 1;
    [SerializeField] float aStarDistance = 3;
    private bool moving;
    private bool waiting;

    [Header("Extra")]
    [SerializeField] float extraDown = 3f;
    [SerializeField] float extraRight = -1f;

    [Header("Audio")]
    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;

    List<Vector2> aStarPath;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        waiting = true;
        moving = false;
        //Debug.Log("Corner-1 : " + corner1.position.x + " | " + corner1.position.z);
        //Debug.Log("Corner-2 : " + corner2.position.x + " | " + corner2.position.z);
        //Debug.Log("Corner-3 : " + corner3.position.x + " | " + corner3.position.z);
        //Debug.Log("Corner-4 : " + corner4.position.x + " | " + corner4.position.z);
        bossStart = false;

        anim = GetComponent<Animator>();
        health = maxHealth;
        firingState = false;
    }

    // Update is called once per frame
    void Update() 
    {
        if(bossStart)
        {
        if (health <= 0)
        {
            VictoryScript.isVictory = true;
        }

        healthSlider.value = health / 2;

      if(waiting)
       StartCoroutine(coroutine());

        
            if(firingState)
            {
                Firing();

            }
       if(moving)
            {
                //Debug.Log("aStarPath Count : " + aStarPath.Count);
                GoPath2(aStarPath);
            }
        }

    }
    public void Firing()
    {
        transform.LookAt(mainCharacter);
        if (ShouldFire())
        {
            Fire();
        }
    }
    IEnumerator coroutine()
    {
        waiting = false;

        Debug.Log("Shooting State");
        transform.LookAt(mainCharacter);
        firingState = true;
        anim.SetBool("Shooting", true);

        yield return new WaitForSeconds(5);
        Debug.Log("Moving State");
        firingState = false;
        anim.SetBool("Shooting", false);
        anim.SetBool("Running", true);

        float randomX = Random.RandomRange(corner3.position.x, corner2.position.x);
        float randomZ = Random.RandomRange(corner3.position.z, corner1.position.z);
        Debug.Log("Random point : " + randomX + " | " + randomZ);
        aStarPath = SearchAStar(transform, randomX, randomZ);
        Debug.Log("aStarPath Count : " + aStarPath.Count);
        moving = true;

        yield return new WaitForSeconds(5);
        moving = false;
        anim.SetBool("Running", false);
        waiting = true;

    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public void Attack()
    {
        anim.SetBool("running", false);
        anim.SetBool("shooting", true);
        if (ShouldFire())
        {
            Fire();
        }
    }
    public bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate)
        {
            return false;
        }
        return true;
    }
    public void Fire()
    {
        audioSource.PlayOneShot(gunShot);

        GameObject currentBullet = Instantiate(bullet, spawnBullet.position, spawnBullet.rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        Vector3 pos = mainCharacter.transform.position;
        pos.y -= extraDown;
        pos.x += extraRight;
        rb.velocity = (pos - transform.position).normalized * bulletSpeed;
        //rb.AddForce(spawnBullet.forward * bulletSpeed, ForceMode.Impulse);
        fireRateTimer = 0;
    }

    private void GoPath2(List<Vector2> aStarPath)
    {

        Vector3 nextPoint;
        if (aStarPath.Count != 0)
        {
            nextPoint = new Vector3(aStarPath[0].x, this.transform.position.y, aStarPath[0].y);
            var lookTo = nextPoint - transform.position;
            lookTo.y = 0;
            var rotation = Quaternion.LookRotation(lookTo);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);


            transform.position = Vector3.Lerp(transform.position, nextPoint, chaseSpeed * Time.deltaTime);
            if (GetDistance(this.transform.position.x, this.transform.position.z, aStarPath[1].x, aStarPath[1].y) < aStarDistance)
            {
                Debug.Log("Removed : " + aStarPath[0].x + " " + aStarPath[0].y);
                aStarPath.RemoveAt(0);
            }
            else
            {
                Debug.Log("Skipped");
            }
        }
    }

}
