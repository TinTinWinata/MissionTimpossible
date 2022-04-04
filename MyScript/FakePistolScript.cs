using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePistolScript : MonoBehaviour
{
    [SerializeField] GameObject weaponFab;
    private bool wait;
    [SerializeField] float waitTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponManager.isHavingWeaponTwo && !WeaponManager.isWeaponTwoActive)
        {
            if(wait)
                StartCoroutine(DelayActive());

        }
        else
        {
            if(wait)
                StartCoroutine(DelayUnactive());

        }

    }
    IEnumerator DelayActive()
    {
        wait = false;
        //Debug.Log("Before Wait");

        yield return new WaitForSeconds(waitTime);
        //Debug.Log("After Wait");
        weaponFab.SetActive(true);
        wait = true;

    }
    IEnumerator DelayUnactive()
    {
        wait = false;
        yield return new WaitForSeconds(waitTime);
        weaponFab.SetActive(false);
        wait = true;
    }
}
