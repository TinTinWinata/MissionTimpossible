using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{

    [SerializeField] private LayerMask ground;
    static public bool isGrounded;

    static readonly float gravity = -9.8f;
    [SerializeField] private float groundDistance;
    private CharacterController cont;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        cont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, 2, ground);
         float ySpeed = -5f;
        ySpeed += gravity * Time.deltaTime;
        //Debug.Log("First : " + ySpeed);

        if (isGrounded && ySpeed < 0)
        {
            ySpeed = -0.8f;
        }

        Vector3 velocity = new Vector3(0, 0, 0);
        velocity.y = ySpeed;
        //Debug.Log(ySpeed);
        cont.Move(velocity * Time.deltaTime);
    }
    public float getGravity()
    {
        return gravity;
    }
}
