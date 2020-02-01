using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deAcceleration;

    [SerializeField] bool secondPlayer;
    bool stopMoving = false;

    Rigidbody rb;
    Vector3 input;
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
        float vertical = secondPlayer == false ? Input.GetAxisRaw("Vertical1") : Input.GetAxisRaw("Vertical2");

        Vector2 beforeInput = new Vector2(horizontal, vertical);
      
        if (!secondPlayer)
        {
            if (hinput.gamepad[0].leftStick.position != Vector2.zero)
            {
                beforeInput = hinput.gamepad[0].leftStick.position;
            }
        }
        else if(secondPlayer)
        {
            if (hinput.gamepad[1].leftStick.position != Vector2.zero)
            {
                beforeInput = hinput.gamepad[1].leftStick.position;
            }
        }

        if (stopMoving)
        {
            beforeInput = Vector2.zero;
        }

        input = new Vector3(GetAcceleratedInput(beforeInput.x, input.x), 0f , GetAcceleratedInput(beforeInput.y, input.z));

        Vector3 moveDirection = input.normalized * movementSpeed;

        rb.velocity = moveDirection;
    }

    void RotateTowardsVelocity()
    {
        transform.rotation = Quaternion.LookRotation(rotVelocity, Vector3.up);
    }

    float GetAcceleratedInput(float newInput, float oldInput)
    {
        if (Mathf.Abs(newInput) > 0)
        {
            //int direction = SetDirection(newInput, oldInput);

            float speed = movementSpeed;

            oldInput = Mathf.MoveTowards(oldInput, newInput, acceleration * Time.deltaTime);
        }
        else
        {
            oldInput = Mathf.MoveTowards(oldInput, 0f, deAcceleration * Time.deltaTime);
        }

        return oldInput;
    }

    int SetDirection(float value, float input)
    {
        int direction = 0; //Make A Variable That Will Store The Direction Of The Player, Depending On Where They Press
        if (value > 0) //If The Player Pressed "Right"
        {
            direction = 1;
        }
        else if (value < 0) //If The Player Pressed "Left"
        {
            direction = -1;
        }

        if (stopMoving == true)
        {
            direction = 0;
            input = 0;
        }

        return direction;
    }

    public void StopMovement()
    {
        stopMoving = true;
    }

    public void ContinueMovement()
    {
        stopMoving = false;
    }
}
