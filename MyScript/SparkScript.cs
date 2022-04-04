using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : MonoBehaviour
{
    // Start is called before the first frame update
    float lifeTime = 0.05f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= (1 * Time.deltaTime); 

        if(lifeTime <= 0)
        {
            Destroy(this);
        }
    }
}
