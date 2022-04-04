using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem particle;
    Ray ray;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MP4Script.firing)
        {
            if(Physics.Raycast(ray, out hit))
            {
                particle.transform.position = hit.point;
                particle.transform.forward = hit.normal;
                particle.Emit(1);

            }
        }
    }
}
