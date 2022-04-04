using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool inputEnabled = true;
    [SerializeField] Animator mainAnim;
    [SerializeField] GameObject playerUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!inputEnabled)
        {
            mainAnim.SetFloat("Velocity Z", 0);
            mainAnim.SetFloat("Velocity X", 0);
            playerUI.SetActive(false);
            PressFScript.pressFOn = false;
        }
        else if (inputEnabled)
        {
            playerUI.SetActive(true);
            PressFScript.pressFOn = true;
        }
    } 
   public static void DialogOff()
    {
        DialogScript.space = true;
    }
}
