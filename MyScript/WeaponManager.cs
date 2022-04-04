using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject weapon1;
    [SerializeField] GameObject weapon2;
    CharacterRiggingScript myRig;

    public static bool isWeaponOneActive;
    public static bool isWeaponTwoActive;

    public static bool isHavingWeaponOne;
    public static bool isHavingWeaponTwo;

    public static int characterDamage;

    public static bool getInputOne;
    public static bool getInputTwo;
    public static bool getInputThree;

    [SerializeField] float changeWeaponTime = 1;
    [SerializeField] float offRigDelay = 1;
    private bool wait;
    
    // Start is called before the first frame update
    void Start()
    {
        wait = true;
        myRig = GetComponent<CharacterRiggingScript>();
        isHavingWeaponOne = true;
        isHavingWeaponTwo = true;

        unarmed();
    }

    // Update is called once per frame
    void Update()
    {
        getInputOne = Input.GetKeyDown(KeyCode.Alpha1);
        getInputTwo = Input.GetKeyDown(KeyCode.Alpha2);
        getInputThree = Input.GetKeyDown(KeyCode.Alpha3);


        if (getInputOne)
        {
            if(isHavingWeaponOne)
            {

              myRig.weightUp();

              getWeapon1();

            }
            getInputOne = false;
        }
        if (getInputTwo)
        {
            if(isHavingWeaponTwo)
            {
                myRig.weightUp();
                getWeapon2();
            }
            getInputTwo = false;
        }
        if (getInputThree)
        {
            unarmed();
            getInputThree = false;
        }

        
    }

    public void getWeapon1()
    {
        characterDamage = 35;
        //CharacterRiggingScript.equippingPistol = true;

        if (isWeaponTwoActive)
        {
            CharacterRiggingScript.holsteringEquipRiffle = true;
            //CharacterRiggingScript.holsteringRiffle = true;
        }

        isWeaponOneActive = true;
        isWeaponTwoActive = false;

        if(wait)
            {
        StartCoroutine(SetActiveWeaponOne());

        }

    }
    IEnumerator SetActiveWeaponOne()
    {
        wait = false;
        yield return new WaitForSeconds(changeWeaponTime);
        weapon1.SetActive(true);
        weapon2.SetActive(false);
        wait = true;
    }
    IEnumerator SetActiveWeaponTwo()
    {
        wait = false;
        yield return new WaitForSeconds(changeWeaponTime);
        weapon2.SetActive(true);
        weapon1.SetActive(false);
        wait = true;
    }


    public void getWeapon2()
    {
        characterDamage = 70;
        //CharacterRiggingScript.equippingRiffle = true;

        if (isWeaponOneActive)
        {
            CharacterRiggingScript.holsteringEquipPistol = true;
            //CharacterRiggingScript.holsteringPistol = true;
        }

        isWeaponTwoActive = true;
        isWeaponOneActive = false;


        if (wait)
            StartCoroutine(SetActiveWeaponTwo());
    }
    public void unarmed()
    {
        if (isWeaponOneActive)
        {
            CharacterRiggingScript.holsteringRiffle = true;
        }
        else if (isWeaponTwoActive)
        {
            CharacterRiggingScript.holsteringPistol = true;
        }

        isWeaponOneActive = false;
        isWeaponTwoActive = false;

        weapon1.SetActive(false);
        weapon2.SetActive(false);

        
        if(wait)
        {
            StartCoroutine(rigDelay());
        }
    }

    IEnumerator rigDelay()
    {
        wait = false;
        yield return new WaitForSeconds(offRigDelay);
        CharacterRiggingScript.targetWeight = 0f;
        wait = true;

    }

    public void active()
    {
        if(isWeaponOneActive)
        {
            getWeapon1();
        }
        else if(isWeaponTwoActive)
        {
            getWeapon2();
        }
    }
    public bool getIsWeaponOneActive()
    {
        return isWeaponOneActive;
    }
    public bool getIsWeaponTwoActive()
    {
        return isWeaponTwoActive;
    }
}
