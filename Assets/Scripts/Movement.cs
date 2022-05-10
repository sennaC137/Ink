using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    bool jump;
    bool isTimeToJump;
    int jumpFrameCount;
    [SerializeField] int framesTilJump = 60;
    [SerializeField] float boostSpeed = 1000f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] bool animation = true;

    void Start()
    {
        jumpFrameCount = framesTilJump;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isTimeToJump && animation)
        {
           GetComponent<Renderer>().material.color = Color.yellow; 
        }
        ProcessRotation();
        CheckIfJumped();
    }

    void FixedUpdate()
    {
        jumpFrameCount ++;
        CheckIfTimeToJump();
        if (isTimeToJump && jump)
        {
            if (animation)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            ProcessBoost();        
            jump = false;
            isTimeToJump = false;
            jumpFrameCount = 0;
        }
    }



    void ProcessBoost()
    {
        rb.AddRelativeForce(Vector3.up * boostSpeed * Time.deltaTime);
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // unfreezing rotation so the physics system can take over
    }

    void CheckIfTimeToJump()
    {
        if (jumpFrameCount > framesTilJump)
        {
            isTimeToJump = true;
        }
    }

    void CheckIfJumped()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTimeToJump)
        {
            jump = true;
        }
    }
}
