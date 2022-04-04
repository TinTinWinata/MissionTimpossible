using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilScript : MonoBehaviour
{

    [SerializeField] Transform cameraLookAt;
    [SerializeField] Transform recoilFollowPos;
    [SerializeField] float kickBackAmount = -1;
    [SerializeField] float kickBackSpeed = 10, returnSpeed = 20;
    float currentRecoilPosition, finalRecoilPosition;
    // Start is called before the first frame update


    private Vector3 currentRotation;
    private Vector3 targetRotation;


    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnYSpeed;

    [SerializeField] private float minRecoil = -0.3f;
    [SerializeField] private float maxRecoil = -1.3f;

    AimScript aimScript;

    void Start()
    {
        aimScript = GetComponentInParent<AimScript>();
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnYSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        cameraLookAt.localRotation = Quaternion.Euler(currentRotation);

        currentRecoilPosition = Mathf.Lerp(currentRecoilPosition, 0, returnSpeed * Time.deltaTime);
        finalRecoilPosition = Mathf.Lerp(finalRecoilPosition, currentRecoilPosition, kickBackSpeed* Time.deltaTime);
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPosition);
    }

    public void TriggerRecoil()
    {
        currentRecoilPosition += kickBackAmount;
        targetRotation += new Vector3(0,Random.Range(-recoilY, recoilY),0);
        aimScript.yAxis.Value += Random.RandomRange(minRecoil, maxRecoil);
    }
}
