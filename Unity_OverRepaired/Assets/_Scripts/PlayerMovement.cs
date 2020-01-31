using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deAcceleration;

    [SerializeField] bool secondPlayer;

    Rigidbody rb;
    Vector3 velocity;
    Vector3 rotVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotVelocity = Vector3.back;
    }

    void Update()
    {
        Move();
        RotateTowardsVelocity();

        if(rb.velocity != Vector3.zero)
        {
            rotVelocity = rb.velocity;
        }
    }


    void Move()
    {
        float horizontal = secondPlayer == false ? Input.GetAxisRaw("Horizontal1") : Input.GetAxisRaw("Horizontal2");
        float vertical =  secondPlayer == false ? Input.GetAxisRaw("Vertical1") : Input.GetAxisRaw("Vertical2");

        velocity = SetVelocity(GetDirection(horizontal), GetDirection(vertical));

        rb.velocity = velocity.normalized * movementSpeed;
    }

    void RotateTowardsVelocity()
    {
        transform.rotation = Quaternion.LookRotation(rotVelocity, Vector3.up);
    }


    Vector3 SetVelocity(float inputx, float inputy)
    {
        Vector3 input = new Vector3(inputx, 0f, inputy);

        if(Mathf.Abs(input.sqrMagnitude) > 0)
        {
            
        }
        else
        {

        }


        return input;
    }


    

    int GetDirection(float input)
    {
        if(input > 0)
        {
            return 1;
        }
        else if(input < 0)
        {
            return -1;
        }
        return 0; 
    }
}
