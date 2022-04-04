using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyScript : MonoBehaviour
{

    [SerializeField] int maxHealth;
    private int health;
    [SerializeField] Transform targetPlayer;
    private Slider slider;
    private bool lookPlayer;

    [SerializeField] GameObject pistolAmmo;
    [SerializeField] GameObject riffleAmmo;
    [SerializeField] Transform deathHere;
    bool alreadyDropAmmmo;
    CharacterController cont;

    public bool death;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        alreadyDropAmmmo = false;
        lookPlayer = true;
        slider = GetComponentInChildren<Slider>();
        health = maxHealth;
        slider.value = maxHealth;
        cont = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        slider.value = health;

        if (health <= 0)
        {
            death = true;
            GenerateAmmo();

            cont.Move(transform.up * 10f * Time.deltaTime);

            StartCoroutine(Gone());
        }
        if(lookPlayer && targetPlayer != null)
        {
            transform.LookAt(targetPlayer);
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
    }
    
    IEnumerator Gone()
    {

        //Vector3 position = this.transform.position;
        //position.y -= 5f;
        //Vector3.Lerp(this.transform.position, position, 100 * Time.deltaTime);

        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    public void lookPlayerFalse()
    {
        lookPlayer = false;
    }
    public void lookPlayerTrue()
    {
        lookPlayer = true;
    }

    public void GenerateAmmo()
    {
        if(!alreadyDropAmmmo)
        {
            QuestScript.enemyDied += 1;
            TimerScript.countDeathEnemy += 1;
            bool gatcha;
        gatcha = Random.value > 0.5;
        if(gatcha)
        {
            bool gatcha2;
            gatcha2 = Random.value > 0.5;
            if(gatcha2)
            {
                SpawnPistolAmmo();
            }
            else
            {
                SpawnRiffleAmmo();
            }
        }
            alreadyDropAmmmo = true;
        }
    }
    public void SpawnPistolAmmo()
    {

        GameObject temp = Instantiate(pistolAmmo);
        temp.transform.position = this.transform.position;
        temp.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);
;    }
    public void SpawnRiffleAmmo()
    {
        GameObject temp = Instantiate(riffleAmmo);
        temp.transform.position = this.transform.position;
        temp.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);
        
    }

}
