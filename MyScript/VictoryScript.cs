using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VictoryScript : MonoBehaviour
{

    public static bool isVictory;
    [SerializeField] CanvasGroup victoryUIGroup;
    [SerializeField] GameObject victoryUI;

    // Start is called before the first frame update
    void Start()
    {
        victoryUI.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {
        if (isVictory)
        {
            Victory();
        }
    }

    public void Victory()
    {
        CursorScript.cursor = true;
        MusicManagerScript.playSound = "after";
        victoryUI.SetActive(true);
        if (victoryUIGroup.alpha < 1)
        {
            victoryUIGroup.alpha += Time.deltaTime;
        }

    }

}
