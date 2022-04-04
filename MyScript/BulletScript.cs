using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody bulletRigidbody;
    private int bulletDamage;
    [SerializeField] Collision ignoreCollider;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] float timeToDestroy;
    private float timer;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyScript enemy = collision.transform.GetComponent<EnemyScript>();
            enemy.takeDamage(WeaponManager.characterDamage);
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript player = collision.transform.GetComponent<PlayerScript>();
            player.takeDamage(1);
        }
        if (collision.gameObject.tag == "Target")
        {
            QuestScript.targetShooted += 1;
        }
        if (collision.gameObject.tag != "Bullet")
        {
            Destroy(this.gameObject);
        } 
        if(collision.gameObject.tag == "IgnoreBullet")
        {
            //Physics.IgnoreCollision(collision)
        }
        if(collision.gameObject.tag == "Roboss")
        {
            RobossScipt roboss = collision.transform.GetComponent<RobossScipt>();
            roboss.takeDamage(WeaponManager.characterDamage);
        }
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}
