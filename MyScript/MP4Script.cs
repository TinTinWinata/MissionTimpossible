using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP4Script : MonoBehaviour
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
    [SerializeField] ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;

    RaycastScript myRayCast;
    // Import from RayCast

    [Header("Reload Animation")]
    [SerializeField] Animator riggingAnimator;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;

    public static bool shouldFire = true;
    public static bool firing;

    private RecoilScript weaponRecoil;
    private SpreadScript weaponSpread;
    public static int addRiffleAmmo;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        addRiffleAmmo = 0;
        weaponSpread = GetComponent<SpreadScript>();
        weaponRecoil = GetComponent<RecoilScript>();
        currentAmmo = clipSize;
        audioSource = GetComponent<AudioSource>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;

        muzzleFlashLight.intensity = 0;
        myRayCast = GetComponentInParent<RaycastScript>();
        fireRateTimer = fireRate;

        muzzleFlashParticles.Play(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (addRiffleAmmo > 0)
        {
            addAmmo(addRiffleAmmo);
            addRiffleAmmo = 0;
        }
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);

        if(GameRuleScript.inputEnabled)
        {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (ShouldFire() && shouldFire)
        {
            Fire();
        }
        }
    }
    void Reload()
    {
        riggingAnimator.SetTrigger("Reload");
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;

            currentAmmo += ammoToReload;

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
          if (Input.GetButton("Fire1"))
        {
            return true;
        }
        }
        return false;
    }
    void Fire()
    {
        anim.SetTrigger("ShootRiffle");
        weaponRecoil.TriggerRecoil();
        if(QuestScript.questNumber == 3)
        {
        QuestScript.bulletShooted += 1;
        }

        TriggerMuzzleFlash();

        fireRateTimer = 0;
        barrelPos.LookAt(myRayCast.getAimPos());
        barrelPos.localEulerAngles = weaponSpread.Spread(barrelPos);
        audioSource.PlayOneShot(gunShot);

        float randomXRecoil = Random.RandomRange(0f, 100f);

        Quaternion rotation = Quaternion.Euler(barrelPos.rotation.x + randomXRecoil, barrelPos.rotation.y, barrelPos.rotation.z);

        rotation = Quaternion.RotateTowards(rotation, Random.rotation, randomXRecoil);

        //Debug.Log("Fire Rotation : " + rotation.x + " " + rotation.y + " " + rotation.z);
        GameObject currentBullet = Instantiate(bullet, barrelPos.position, rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        rb.AddForce(barrelPos.forward * bulletSpeed, ForceMode.Impulse);


        currentAmmo -= 1;;
        firing = false;
    }



    void TriggerMuzzleFlash()
    {
        muzzleFlashLight.intensity = lightIntensity;
        muzzleFlashParticles.Play(true);
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
        return currentAmmo + "/" + extraAmmo ;
     }
    public void addAmmo(int ammo)
    {
        extraAmmo += ammo;
    }

}
