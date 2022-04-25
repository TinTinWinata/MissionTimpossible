using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void resetGame()
    {
        DeathScript.isDead = false;
        CursorScript.cursor = false;
        QuestScript.questNumber = 0;
        QuestScript.targetShooted = 0;
        QuestScript.bulletShooted = 0;
    }   
}
