using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TakeAmmoScript : MonoBehaviour
{
    [SerializeField] LayerMask ammoMask;
    [SerializeField] pistolScript pistol;
    [SerializeField] MP4Script m4;
    [SerializeField] TMP_Text txtTakeAmmo;
    public bool onPressF;
    bool ifHitPistolAmmo, ifHitRiffleAmmo, ifHitRiffle, ifHitPistol;
    [SerializeField] LayerMask riffleAmmo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(centerScreen);


        ifHitRiffleAmmo = false;
        ifHitPistolAmmo = false;
        ifHitRiffle = false;
        ifHitPistol = false;


        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ammoMask))
        {
            if (hit.collider.gameObject.tag == "PistolAmmo")
            {
                ifHitPistolAmmo = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    pistol.addAmmo(7);
                    Destroy(hit.transform.gameObject);
                }
                txtTakeAmmo.text = "Press F To Take Pistol Ammo";
            }
            else if (hit.collider.gameObject.tag == "RiffleAmmo")
            {
                ifHitRiffleAmmo = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    m4.addAmmo(30);
                    Destroy(hit.transform.gameObject);
                }
                txtTakeAmmo.text = "Press F To Take Riffle Ammo";
            }
            else if (hit.collider.gameObject.tag == "Riffle")
            {
                if(QuestScript.questNumber == 3)
                {
                  ifHitRiffle = true;
                  if (Input.GetKeyDown(KeyCode.F))
                  {
                    WeaponManager.isHavingWeaponOne = true;
                    Destroy(hit.transform.gameObject);
                        WeaponManager.getInputOne = true;
                    }
                    txtTakeAmmo.text = "Press F To Take Riffle";
                }   
            }
            else if (hit.collider.gameObject.tag == "Pistol")
            {
                if(QuestScript.questNumber == 1)
                {

                ifHitPistol = true;
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        WeaponManager.isHavingWeaponTwo = true;
                        Destroy(hit.transform.gameObject);
                     QuestScript.done = true;
                        QuestScript.onGoing = false;
                        WeaponManager.getInputTwo = true;
                    }
                    txtTakeAmmo.text = "Press F To Take Pistol";
                }
            }


        }
        onPressF = ifHitRiffleAmmo || ifHitPistolAmmo || ifHitPistol || ifHitRiffle;

    }
    public bool getOnPressF()
    {   
        return onPressF;
    }
}
