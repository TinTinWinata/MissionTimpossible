using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressFScript : MonoBehaviour
{

    private TakeAmmoScript takeAmmo;
    [SerializeField] GameObject pressF;
    [SerializeField] TeleporterScript teleporter;
    [SerializeField] AsunaScript asuna;
    private TMP_Text txtTakeAmmo;
    public static bool pressFOn;
    


    // Start is called before the first frame update
    void Start()
    {
        takeAmmo = GetComponent<TakeAmmoScript>();
        txtTakeAmmo = pressF.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (takeAmmo.getOnPressF())
        {
            pressF.SetActive(true);
        }
        else if(teleporter.getOnPressF())
        {
            txtTakeAmmo.text = "Press F to Teleport";
            pressF.SetActive(true);
        }
        else if(asuna.getOnPressF())
        {
            txtTakeAmmo.text = "Press F to Interact";
            pressF.SetActive(true);
        }
        else if(RiffleAmmoScript.checkPlayerInRadius)
        {
            txtTakeAmmo.text = "Press F to Interact";
            pressF.SetActive(true);
        }
        else if(pressF)
        {
            pressF.SetActive(false);
        }
        else
        {
            pressF.SetActive(false);
        }

    }
}
