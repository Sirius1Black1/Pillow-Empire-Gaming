using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
//Code based on Brackeys (https://www.youtube.com/watch?v=4HpC--2iowE) 
{

    private CharacterController controller;

    public Transform cam;

    public float speed = 5f;
    public float runSpeed = 8f;
    public float sneakSpeed = 3f;

    public float turnSpeed = 0.1f;
    float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        //Since the Controller is a Component that is on the Player I would make it a required component -> less room for error
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
<<<<<<< Updated upstream

        if (direction.magnitude > joystickDeadzone)
=======
        float targAtangle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targAtangle, ref turnSmoothVelocity, turnSpeed);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, targAtangle, 0f) * Vector3.forward;
            
        if (Input.GetKey(KeyCode.LeftShift))
>>>>>>> Stashed changes
        {
            controller.Move(moveDir.normalized * runSpeed * Time.deltaTime * direction.magnitude);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.Move(moveDir.normalized * sneakSpeed * Time.deltaTime * direction.magnitude);
        }
        else
        {
            controller.Move(moveDir.normalized * speed * Time.deltaTime * direction.magnitude);
        }
        


    }
}
