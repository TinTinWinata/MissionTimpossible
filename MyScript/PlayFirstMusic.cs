using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFirstMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManagerScript.playSound = "game";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
