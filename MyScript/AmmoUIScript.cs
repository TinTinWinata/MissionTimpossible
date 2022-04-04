using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUIScript : MonoBehaviour
{

    [SerializeField] TMP_Text ammo;
    [SerializeField] MP4Script mp4;
    [SerializeField] pistolScript pistol;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] Image weaponOneImage;
    [SerializeField] Image weaponTwoImage;


    // Start is called before the first frame update
    void Start()
    {
        DeactivateColor();
    }

    // Update is called once per frame
    void Update()
    {
     
        if (weaponManager.getIsWeaponOneActive())
        {

            SetColorActive(1);
            ammo.text = mp4.getStringAmmo();
        }
        else if(weaponManager.getIsWeaponTwoActive())
        {
            SetColorActive(2);
            ammo.text = pistol.getStringAmmo();
        } else
        {
            DeactivateColor();
            ammo.text = "";
        }
    }
    void SetColorActive(int weapon)
    {
        if(weapon == 1)
        {
            var tempColor = weaponOneImage.color;
            tempColor.a = 1f;
            weaponOneImage.color = tempColor;

            var tempColor2 = weaponTwoImage.color;
            tempColor2.a = 0.4f;
            weaponTwoImage.color = tempColor2;
        }
        else if (weapon == 2)
        {
            var tempColor = weaponOneImage.color;
            tempColor.a = 0.4f;
            weaponOneImage.color = tempColor;

            var tempColor2 = weaponTwoImage.color;
            tempColor2.a = 1f;
            weaponTwoImage.color = tempColor2;
        }
    }
    void DeactivateColor()
    {
        var tempColor = weaponOneImage.color;
        tempColor.a = 0.4f;
        weaponOneImage.color = tempColor;

        var tempColor2 = weaponTwoImage.color;
        tempColor2.a = 0.4f;
        weaponTwoImage.color = tempColor2;
    }
}
