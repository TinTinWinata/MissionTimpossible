using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathScript : MonoBehaviour
{

    public static bool isDead;
    [SerializeField] CanvasGroup deathUiGroup;
    [SerializeField] GameObject deathUi;
    // Start is called before the first frame update
    void Start()
    {
        deathUi.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {
        if(isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        CursorScript.cursor = true;
        MusicManagerScript.playSound = "after";
        deathUi.SetActive(true);
        if(deathUiGroup.alpha < 1) 
        {
            deathUiGroup.alpha += Time.deltaTime;
        }
    }

}
