using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [Header("Player Attribute")]
    [SerializeField] private int maxHealth = 100;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(Vector3.back);
        Debug.Log("WOW");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            DeathScript.isDead = true;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            health = 0;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public int getHealth()
    {
        return health;
    }
    public float getFloatHealth()
    {
        return health / 100;
    }
    public void resetHealth()
    {
        health = maxHealth;
    }
   
}
