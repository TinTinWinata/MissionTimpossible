using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimScript : MonoBehaviour
{

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;


    [SerializeField] Transform camFollowPos;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if(GameRuleScript.inputEnabled)
        {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
        }
    }
    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }

}
