using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera zoomCamera;
    

    // Start is called before the first frame update
    private bool getInputRightMouse;
    public static bool zooming;
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        getInputRightMouse = Input.GetMouseButton(1);
        if (getInputRightMouse)
        {
            zooming = true;
            zoomCamera.gameObject.SetActive(true);
        }
        else
        {
            zooming = false;
            zoomCamera.gameObject.SetActive(false);
        }

    }
}