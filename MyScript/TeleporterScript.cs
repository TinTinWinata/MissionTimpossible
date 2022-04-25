using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool checkPlayerInRadius;
    private int radius = 15;
    public static bool finished;

    [SerializeField] LayerMask person;
    [SerializeField] GameObject player;
    [SerializeField] PlayerScript playerScript;
    void Start()
    {
        radius = 25;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerInRadius = Physics.CheckSphere(transform.position, radius, person);

        if (finished)
        {

            if (checkPlayerInRadius)
         {
            if (Input.GetKey(KeyCode.F))
            {
                playerScript.resetHealth();  
                    player.transform.position = MazeGeneratorScript.spawnVector;
            }
         }
        }

    }
    public bool getOnPressF()
    {
        return checkPlayerInRadius;
    }

}
