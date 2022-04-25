using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RiffleAmmoScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool checkPlayerInRadius;
    private int radius = 15;
    public static bool finished;

    [SerializeField] LayerMask person;
    [SerializeField] MP4Script mp4;
    [SerializeField] PressFScript pressFScript;
    void Start()
    {
        radius = 5;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerInRadius = Physics.CheckSphere(transform.position, radius, person);
        if (checkPlayerInRadius)
        {
            pressFScript.PressOn();
            if (Input.GetKeyDown(KeyCode.F))
            {
                mp4.addAmmo(30);
                checkPlayerInRadius = false;
                Destroy(this.gameObject);
            }
        }
        else
        {
            pressFScript.PressOff();
        }
    }
}
