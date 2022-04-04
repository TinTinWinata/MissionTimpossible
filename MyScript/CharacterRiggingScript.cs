using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class CharacterRiggingScript : MonoBehaviour
{
    // Start is called before the first frame update

    Rig aimRig;
    [SerializeField] Animator anim;
    [SerializeField] Animator mainAnim;
    public static float targetWeight;
    WeaponManager weaponManager;


    // -----------------

    public static bool holsteringPistol;
    public static bool holsteringRiffle;

    public static bool equippingRiffle;
    public static bool equippingPistol;

    public static bool reloadingPistol;
    public static bool reloadingRiffle;

    public static bool jumping;

    public static bool holsteringEquipRiffle;
    public static bool holsteringEquipPistol;









    void Start()
    {
        aimRig = GetComponentInChildren<Rig>();
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateRig();

        if(holsteringPistol)
        {
            holsterPistol();
            holsteringPistol = false;
        }
        else if (holsteringRiffle)
        {
            holsterRiffle();
            holsteringRiffle = false;
        }
        if(equippingPistol)
        {
            equipPistol();
            equippingPistol = false;
        }else if(equippingRiffle)
        {
            equipRiffle();
            equippingRiffle = false;
        }
        if(reloadingRiffle)
        {
            reloadRiffle();
            reloadingRiffle = false;
        }
        if(reloadingPistol)
        {
            reloadPistol();
            reloadingPistol = false;
        } 
        if(jumping)
        {
            mainAnim.SetTrigger("Jump");
            jumping = false;
        }
        if(holsteringEquipRiffle)
        {
            holsterEquipRiffle();
            holsteringEquipRiffle = false;
        }
        if(holsteringEquipPistol)
        {
            holsterEquipPistol();
            holsteringEquipPistol = false;
        }
    }
    public void reloadRiffle()
    {
        anim.SetTrigger("ReloadRiffle");

    }
    public void reloadPistol()
    {
        anim.SetTrigger("ReloadPistol");

    }
    public void holsterPistol()
    {
        anim.SetTrigger("HolsterPistol");
    }

    public void equipPistol()
    {
        anim.SetTrigger("EquipPistol");
    }
    public void equipRiffle()
    {
        anim.SetTrigger("EquipRiffle");

    }
    public void holsterRiffle()
    {
        anim.SetTrigger("HolsterRiffle");
    }    
    public void holsterEquipRiffle()
    {
        anim.SetTrigger("HolsterEquipRiffle");
    }

    public void holsterEquipPistol()
    {
        anim.SetTrigger("HolsterEquipPistol");
    }
    public void calculateRig()
    {
        aimRig.weight = Mathf.Lerp(aimRig.weight, targetWeight, Time.deltaTime * 10f);

    }
    public void weightUp()
    {
        
        targetWeight = 1f;
    }
}
