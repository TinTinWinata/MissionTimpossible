using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManagerScript : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponManager.isWeaponOneActive || WeaponManager.isWeaponTwoActive)
        {
            anim.speed = 1f;
        }
        else if(ZoomScript.zooming)
        {
            anim.speed = 0.5f;
        }
        else
        {
            anim.speed = 1.3f;
        }
    }
}
