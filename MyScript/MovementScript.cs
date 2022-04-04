using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rig;
    [SerializeField] private Camera cam;
    [SerializeField] float turnSpeed;
    //private CharacterController cont;

    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpSpeed = 3.5f;
    [SerializeField] float ySpeed = 5f;
    [SerializeField] float changeMinus = -0.8f;
    CharacterController cont;

    [SerializeField] LayerMask ground;
    private float jumpTimer = 1;
    [SerializeField] Animator rootAnim;


    // Raycast
    //[SerializeField] private Transform debugTransform;
    //[SerializeField] private LayerMask aimColliderMask = new LayerMask();

    // Start is called before the first frame update
    void Start()
    {
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        // cont = GetComponent<CharacterController>();
       // rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameRuleScript.inputEnabled)
        {
            float getInputVertical = Input.GetAxis("Vertical");
            anim.SetFloat("Velocity Z", getInputVertical);

            float getInputHorizontal = Input.GetAxis("Horizontal");
            anim.SetFloat("Velocity X", getInputHorizontal);

            bool isGrounded = Physics.CheckSphere(transform.position, 2, ground);

            ySpeed += gravity * Time.deltaTime;
            if(isGrounded && ySpeed < 0)
            {
                ySpeed = changeMinus;
                if (Input.GetKeyDown("space") && jumpTimer <= 0)
                  {
                    rootAnim.SetTrigger("Jump"); 
                    CharacterRiggingScript.jumping = true;
                    ySpeed = jumpSpeed;
                    jumpTimer = 1;
                  }   
            }
            jumpTimer -= (1 * Time.deltaTime);

            Vector3 velocity = new Vector3(0, 0, 0);
            velocity.y = ySpeed;
            cont.Move(velocity * Time.deltaTime);
        }
    }
}
