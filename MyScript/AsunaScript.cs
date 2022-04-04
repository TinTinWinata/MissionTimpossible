using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AsunaScript : MonoBehaviour
{
    // Start is called before the first frame update

    private bool checkPlayerInRadius;
    private bool classicDialogue;
    [SerializeField] private float radius = 5;
    [SerializeField] LayerMask person;
    [SerializeField] DialogScript dialogue;

    List<string> dialogueList = new List<string>();



    void Start()
    {
        initiateDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        classicDialogue = false;
        checkPlayerInRadius = Physics.CheckSphere(transform.position, radius, person);
        if (checkPlayerInRadius)
        {
            WeaponManager.getInputThree = true;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if(!QuestScript.done && !QuestScript.onGoing)
                {
                    dialogue.Dialogue(dialogueList[QuestScript.questNumber]);
                    QuestScript.onGoing = true;
                    QuestScript.questNumber += 1;
                    VideoScript.playFilmIndex = QuestScript.questNumber;
                    // Belom ada quest sama sekali
                }
                else if(QuestScript.done && !QuestScript.onGoing)
                {
                    dialogue.Dialogue(dialogueList[QuestScript.questNumber]);
                    QuestScript.onGoing = true;
                    QuestScript.questNumber += 1;
                    QuestScript.done = false;
                    VideoScript.playFilmIndex = QuestScript.questNumber;
                    Debug.Log("Next Quest");
                    // Ada quest dan kelar
                }
                else
                {
                classicDialogue = true;
                }

            }

        }
    }
    void initiateDialogue()
    {
        dialogueList.Add("Go pick up a pistol and start shooting!");
        dialogueList.Add("Comeback when you have mastered the art of shooting!");
        dialogueList.Add("Good Job,now get the rifle and try shooting 50 rounds!.");
        dialogueList.Add("Now, you're ready to got a fight! Now go through the passage and eliminate the enemies!");
        dialogueList.Add("Good job! you're killed all the enemies! Get into the secret teleport room to fight the boss!");

    }
    public bool getOnPressF()
    {
        return checkPlayerInRadius;
    }
    public bool getOnClassicDialogue()
    {
        return classicDialogue;
    }
    
}
