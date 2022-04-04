using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool checkPlayerInRadius;
    [SerializeField] LayerMask person;
    [SerializeField] float radius = 2f;
    [SerializeField] Transform player;
    [SerializeField] Transform teleportTo;
    [SerializeField] GameObject robbosUI;
    private bool temp;
    private bool alreadyDone;
    void Start()
    {
        alreadyDone = false;
        temp = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        checkPlayerInRadius = Physics.CheckSphere(transform.position, radius, person);
        if(!RobossScipt.bossStart)
        {

        if (checkPlayerInRadius)
        {
                MusicManagerScript.playSound = "boss";
                robbosUI.SetActive(true);
            VideoScript.playFilmIndex = 6;
            RobossScipt.bossStart = true;
            player.rotation = teleportTo.rotation;
            player.position = teleportTo.position;
        }
        }
    }
    void Timer(float time)
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            temp = true;
        }
    }
}
