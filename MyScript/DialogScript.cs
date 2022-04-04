using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogScript : MonoBehaviour
{

    [SerializeField] GameObject dialogue;
    [SerializeField] AsunaScript asuna;
    private bool active = false;
    private TMP_Text dialogueTxt;
    public static bool space;


    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(false);
        dialogueTxt = dialogue.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (asuna.getOnClassicDialogue())
        {
            dialogueTxt.text = "You haven't finished your task yet. Come back when you have finished it";
            dialogue.SetActive(true);
            GameRuleScript.inputEnabled = false;
        }
        if (space || Input.GetKeyDown("space"))
        {
            dialogue.SetActive(false);
            GameRuleScript.inputEnabled = true;
            space = false;
        }
    }

    public void Dialogue(string str)
    {
        dialogueTxt.text = str;
        dialogue.SetActive(true);
        GameRuleScript.inputEnabled = false;
    }

    public void Active()
    {
        dialogue.SetActive(true);
        GameRuleScript.inputEnabled = false;
    }
    public static void Space()
    {
        space = true;
    }
}
