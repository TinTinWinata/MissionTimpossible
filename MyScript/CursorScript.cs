using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool cursor;
    [SerializeField] bool startWithCursor;
    void Start()
    {
        cursor = startWithCursor;
        if(startWithCursor)
        {
            CursorOn();
        }
        else if(!startWithCursor)
        {
            CursorOff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cursor)
        {
            CursorOn();
        }
        

    }

    public void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
