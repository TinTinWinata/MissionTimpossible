using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerScript playerScript;
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameRuleScript.inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.H))
                playerScript.resetHealth();
        }
    }
}
