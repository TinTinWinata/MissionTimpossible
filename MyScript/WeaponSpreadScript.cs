using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadScript : MonoBehaviour
{

    [SerializeField] float defaultSpread = 3;
    [SerializeField] float adsMultiplier = 0.5f;

    private float currentSpread;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public Vector3 Spread(Transform barrelPos)
    {
        if (ZoomScript.zooming)
        {
            currentSpread = defaultSpread * adsMultiplier;
        }
        else
        {
            currentSpread = defaultSpread;
        }
        float randX = Random.Range(-currentSpread, currentSpread);
        float randY = Random.Range(-currentSpread, currentSpread);
        float randZ = Random.Range(-currentSpread, currentSpread);

        Vector3 randomRotation = new Vector3(randX, randY, randZ);
        return barrelPos.localEulerAngles + randomRotation;
    }

}
