using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class AIScript : MonoBehaviour
{
    
    static float gridSize = 1f;
    int[] tempX = { 1, 0, -1, 0, 1, 1, -1, -1 };
    int[] tempZ = { 1, 0, -1, 0, 1, 1, -1, -1 };

    int lastX;
    int lastZ;
    public Transform target1, target2;
    Node[,] nodeList;

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

    private Node GetNode(int x, int z)
    {
        if (nodeList[x, z] == null)
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
        nodeList = new Node[500, 500];
        List<Vector2> pathList = new List<Vector2>();
        bool foundTarget = false;

        GetNode(x, z).visited = true;
        List<Node> nodeQueue = new List<Node>();
        nodeQueue.Add(new Node(x, z, target2));

        while (nodeQueue.Count > 0 && !foundTarget)
        {
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
                //Debug.Log("KETEMU");
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
            // Round Checking


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








private Animator anim;
    [SerializeField] int cooldown;
    [SerializeField] Transform spawnBullet;
    private CharacterController cont;


    public Transform playerTransform;

    [SerializeField] LayerMask groundMask, playerMask;
    EnemyScript enemy;


    [Header("Enemy Speed")]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] int waitTime;

    private int timer;

    private bool chasePlayer;
    private bool attackPlayer;
    
    [Header("Enemy Range")]
    [SerializeField] private float chasePlayerRange;
    [SerializeField] private float attackPlayerRange;

    // State

    private bool rotatingLeft;
    private bool rotatingRight;
    private bool running;
    private bool waiting;

    private float aStarTimer = 2;
    private bool nextAstar;

    [Header("Bullet")]
    [SerializeField] GameObject bullet;
    [SerializeField] int bulletSpeed;
    [SerializeField] float fireRate;
    private float fireRateTimer;

    [Header("Astar")]
    [SerializeField] float chaseSpeed;
    [SerializeField] float aStarDistance = 3;
    List<Vector2> aStarPath;

    [Header("Weapon Muzzle Extras")]
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;
    Light muzzleFlashLight;
    [SerializeField] ParticleSystem muzzleFlashParticles;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        nextAstar = true;
        waiting = true;
        rotatingLeft = false;
        rotatingRight = false;
        running = false;

        muzzleFlashLight = GetComponentInChildren<Light>();
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticles.Play(false);
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;

        enemy = GetComponent<EnemyScript>();
        cont = GetComponent<CharacterController>();
        playerTransform = GameObject.Find("MainCharacter").transform;
        timer = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);

            //Debug.Log("NOT DEATH");

            timer += 1;
        chasePlayer = Physics.CheckSphere(transform.position, chasePlayerRange, playerMask);
        attackPlayer = Physics.CheckSphere(transform.position, attackPlayerRange, playerMask);

        enemy.lookPlayerFalse();


        if (attackPlayer)
        {
            //Debug.Log("Attacking State");
            Attack();
        }
        else if (chasePlayer)
        {
            //Debug.Log("Attacking State");

            Chase();
        }
        else
        {
            //Debug.Log("Patrolling State");

            Patroling();
        }
        

    }
    public void Patroling()
    {
        //Debug.Log("Patrolling State");

        if(waiting)
        { 
         StartCoroutine(Wander());      
        }
        if(rotatingRight)
        {
            //Debug.Log("Rotating Right");
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }
        if(rotatingLeft)
        {
            //Debug.Log("Rotating Left")
            rotateEnemy(); 
        }
        if (running)
        {
            anim.SetBool("running", true);
            cont.Move(transform.forward * 3 * Time.deltaTime);
        }
        else
        {
            anim.SetBool("running", false);
        }
  
        

    }

    IEnumerator Wander()
    {
        //Debug.Log("Wandering State");
        int rotationTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int randomRotate = Random.Range(1, 2);
        int walkWait = Random.Range(0, 10) + waitTime;
        int walkTime = Random.Range(1, 3);



        waiting = false;
        yield return new WaitForSeconds(walkWait);


        running = true;
        yield return new WaitForSeconds(walkTime);
        running = false;

        if(randomRotate == 1)
        {
            rotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            rotatingLeft = false;
        }
        else
        {
            rotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            rotatingRight = false;
        }
        waiting = true;
    }
    public void rotateEnemy()
    {
        transform.Rotate(transform.up * Time.deltaTime * 100);
    }



    public void Chase()
    {
        anim.SetBool("running", true);
        anim.SetBool("shooting", false);



        if (aStarTimer == 2)
        {
            aStarPath = SearchAStar(transform, playerTransform);
            nextAstar = false;
            //Debug.Log("Gotted Path : " + aStarPath.Count);
            //StartCoroutine(GoPath(aStarPath));\
        }


        GoPath2(aStarPath);


        aStarTimer -= 1 * Time.deltaTime;
        if (aStarTimer <= 0)
            aStarTimer = 2;

        // A STAR DISINI
    }

    private void GoPath2(List<Vector2> aStarPath)
    {
        Vector3 nextPoint;
        if (aStarPath.Count != 0)
        {
            float y = Terrain.activeTerrain.SampleHeight(transform.position);
            nextPoint = new Vector3(aStarPath[0].x, y, aStarPath[0].y);
            var lookTo = nextPoint - transform.position;
            lookTo.y = 0;
            var rotation = Quaternion.LookRotation(lookTo);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);


            transform.position = Vector3.Lerp(transform.position, nextPoint, chaseSpeed * Time.deltaTime);
            if (GetDistance(this.transform.position.x, this.transform.position.z, aStarPath[1].x, aStarPath[1].y) < aStarDistance)
            {
                aStarPath.RemoveAt(0);
            }
        }
    }
   

    private bool waitBool = true;
    private void trueBool()
    {
        waitBool = true;
    }

    public void Attack()
    {
        anim.SetBool("running", false);
        anim.SetBool("shooting", true);
        enemy.lookPlayerTrue();
        if(ShouldFire() && !enemy.death)
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

        TriggerMuzzleFlash();
        audioSource.PlayOneShot(gunShot);

        GameObject currentBullet = Instantiate(bullet, spawnBullet.position, transform.rotation );
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        fireRateTimer = 0;
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashLight.intensity = lightIntensity;
        muzzleFlashParticles.Play(true);
    }
}
