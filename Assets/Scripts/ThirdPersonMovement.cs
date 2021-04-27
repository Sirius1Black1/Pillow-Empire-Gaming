using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
//Code based on Brackeys (https://www.youtube.com/watch?v=4HpC--2iowE) 
{

    private CharacterController controller;

    public Transform cam;

    [Header("Speeds")]
    public float speed = 5f;
    public float runSpeed = 8f;
    public float sneakSpeed = 3f;
    public float turnSpeed = 0.2f;
    float turnSmoothVelocity;
    public float terminalVel = 100; //max velocity due to some kind of drag (e.g. air drag etc)
    
    [Header("Vector Based System Settings")]
    public bool isLockedToRigidBodyGrav = true;
    public Vector3 vectorGravity = new Vector3(0, 9.81f, 0);
    public Vector3 jumpVector = new Vector3(0, 10, 0);

    private Vector3 vectorSpeed = Vector3.zero;
    private bool Grounded = false;


    // Start is called before the first frame update
    void Start()
    {
        //Since the Controller is a Component that is on the Player I would make it a required component -> less room for error
        controller = GetComponent<CharacterController>();
        //set cam reference
        cam = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //get input direction
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        //smooth rotation
        float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);

        //turn player to face in the movement direction
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Vector along wich to move
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        //Movement mode handling
        if (Input.GetButton("Run"))
        {
            controller.Move(moveDir.normalized * runSpeed * Time.deltaTime * direction.magnitude);
        }
        else if (Input.GetButton("Sneak"))
        {
            controller.Move(moveDir.normalized * sneakSpeed * Time.deltaTime * direction.magnitude);
        }
        else
        {
            controller.Move(moveDir.normalized * speed * Time.deltaTime * direction.magnitude);
        }

        //Jumping
        if (Input.GetButton("Jump") && Grounded)
        {
            vectorSpeed = vectorSpeed + (Quaternion.Euler(0f, angle, 0f) * jumpVector);
        }

        // apply gravity acceleration to vertical speed:
                
        //Vector Gravity
        if (isLockedToRigidBodyGrav)
        {
            vectorSpeed += Physics.gravity * Time.deltaTime;
        }
        else
        {
            vectorSpeed += vectorGravity * Time.deltaTime;
        }

        // terminal velocity due to air resistance
        vectorSpeed = Vector3.ClampMagnitude(vectorSpeed, terminalVel);

        controller.Move(vectorSpeed * Time.deltaTime);

        //always after applying speed
        if (controller.isGrounded)
        {
            // grounded character has vSpeed = 0
            vectorSpeed = Physics.gravity.normalized;
            //TODO: not such a nice solution, but better just validated once then more often
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }
    }
}
