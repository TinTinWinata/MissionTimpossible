using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class QuestScript : MonoBehaviour
{
   
    // Start is called before the first frame update
    [SerializeField] TMP_Text questText;
    List<string> questList = new List<string>();
    public static bool done;
    public static bool onGoing;
    public static int questNumber = 0;
    public static int targetShooted = 0;
    public static int bulletShooted = 0;

    [SerializeField] GameObject villagerEnemy;
    [SerializeField] GameObject teleporterEnemy;

    public static int enemyDied = 0;
    public bool green = false;
    string tempQuestText;

    void Start()
    {
        //villagerEnemy.SetActive(false);
        //teleporterEnemy.SetActive(false);
        InitiateQuestion();
    }

    // Update is called once per frame
    void Update()
    {

        if(questNumber == 2)
        {
            if (targetShooted >= 10)
            {
                done = true;
                onGoing = false;
                targetShooted = 10;
            }
            questText.text = questList[questNumber] + " ("  +targetShooted + " / 10)";
        }
        else if (questNumber == 3)
        {
            if (bulletShooted >= 50)
            {
                done = true;
                onGoing = false;
                bulletShooted = 50;
            }
            questText.text = questList[questNumber] + " (" + bulletShooted + " / 50)";
        }
        else if (questNumber == 4)
        {
            if (enemyDied == 16)
            {
                done = true;
                onGoing = false;
                enemyDied = 0;
            }
            questText.text = questList[questNumber] + " (" + enemyDied + " / 16)";
        }
        else if (questNumber == 5)
        {
            if (enemyDied == 9)
            {
                done = true;
                onGoing = false;
                enemyDied = 0;
            }
            questText.text = questList[questNumber] + " (" + enemyDied + " / 9)";
        }
        else
        {
            questText.text = questList[questNumber];
        }

        if (done)
        {
            
            questText.color = Color.green;
            questText.text = questList[questNumber] + "(done)";
        }
        else if (!done)
        {
            questText.color = Color.yellow;
        }

    }
    public static void Done()
    {
        pistolScript.addPistolAmmo = 7;
        MP4Script.addRiffleAmmo = 30;
        questNumber += 1;
        done = true;
    }
    // gak tau pake apa nggak
    void InitiateQuestion()
    {
        questList.Add("Talk to Asuna");
        questList.Add("Pick up the pistol");
        questList.Add("Shoot 10 rounds to the target!");
        questList.Add("Shoot 50 bullets with the Rifle");
        questList.Add("Eliminate the soldiers that are attacking the village");
        questList.Add("Head to the secret teleport room and defeat the boss!");
    }

    
}
