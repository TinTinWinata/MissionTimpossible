using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update

    private float timer = 60;
    private float currTime;
    private bool teleporterStart = false;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject timerCanvas;

    [SerializeField] TMP_Text txt;
    private bool checkPlayerInRadius;
    [SerializeField] public float radius = 4f;
    [SerializeField] LayerMask person;
    public static bool timerStop;
    private float time = 0;
    public static int totalEnemy = 8;

    public static int countDeathEnemy;

    void Start()
    {
        currTime = timer;
        teleporterStart = false;
        timerStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(countDeathEnemy);

        if (countDeathEnemy >= totalEnemy)
        {
            timerStop = true;
            TeleporterScript.finished = true;
            timerCanvas.SetActive(false);
        }

        checkPlayerInRadius = Physics.CheckSphere(transform.position, radius, person);
        
        if (checkPlayerInRadius)
        {
            teleporterStart = true;
            countDeathEnemy = 0;
        }
        if(teleporterStart)
        {
            triggerActive();
        }
    }


    void triggerActive()
    {
        time += 1 * Time.deltaTime;
        canvas.SetActive(true);
        if(time > 3)
        {
            canvas.SetActive(false);
            Timer();
        }

    }
    void Timer()
    {
        timerCanvas.SetActive(true);
        if(!timerStop)
        {
         currTime -= 1 * Time.deltaTime;
        }
        txt.SetText(currTime.ToString("0.00"));
    }
}
