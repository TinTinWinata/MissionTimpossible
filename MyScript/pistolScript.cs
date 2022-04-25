using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolScript : MonoBehaviour
{

    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletSpeed;


    [Header("Weapon Status")]
    [SerializeField] float weaponDamage;
    [SerializeField] public int clipSize;
    [SerializeField] public int extraAmmo;
    [HideInInspector] public int currentAmmo;

    [Header("Weapon Muzzle Extras")]
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;
    
    public static int addPistolAmmo;

    RaycastScript myRayCast;
    // Import from RayCast

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    Animator anim;
    RecoilScript weaponRecoil;

    // Start is called before the first frame update
    void Start()
    {
        weaponRecoil = GetComponent<RecoilScript>();
        anim = GetComponentInParent<Animator>();
        addPistolAmmo = 0;
        startAttribute();
        currentAmmo = clipSize;
        audioSource = GetComponent<AudioSource>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        muzzleFlashLight.intensity = 0;
        myRayCast = GetComponentInParent<RaycastScript>();
        fireRateTimer = fireRate;
        muzzleFlashParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (addPistolAmmo > 0)
        {
            addAmmo(addPistolAmmo);
            addPistolAmmo = 0;
        }
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);

        if(GameRuleScript.inputEnabled)
        {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (ShouldFire())
        {
            Fire();
         }
        }
    }
    void Reload()
    {
        CharacterRiggingScript.reloadingPistol = true;
        Debug.Log("current ammo : " + currentAmmo);
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;

            currentAmmo += ammoToReload;

            // Make reload in this
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {
                int tempAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = tempAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
            //Special Condition
        }
    }
    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate)
        {
            return false;
        }
        if (currentAmmo <= 0)
        {
            return false;
        }
        if(GameRuleScript.inputEnabled)
        {

        if (Input.GetButtonDown("Fire1"))
        {
            return true;
        }
        }


        return false;
    }
    void startAttribute()
    {
        weaponDamage = 35;
    }
    void Fire()
    {
        weaponRecoil.TriggerRecoil();
        anim.SetTrigger("ShootPistol");
        TriggerMuzzleFlash();

        fireRateTimer = 0;
        barrelPos.LookAt(myRayCast.getAimPos());
        audioSource.PlayOneShot(gunShot);

        GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        rb.AddForce(barrelPos.forward * bulletSpeed, ForceMode.Impulse);


        currentAmmo -= 1;
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashParticles.Play(true);
        muzzleFlashLight.intensity = lightIntensity;

    }
    public int getCurrentAmmo()
    {
        return currentAmmo;
    }
    public int getExtraAmmo()
    {
        return extraAmmo;
    }
    public string getStringAmmo()
    {
        return currentAmmo + "/" + extraAmmo;
    }
    public void addAmmo(int ammo)
    {
        extraAmmo += ammo;
    }
    

}
