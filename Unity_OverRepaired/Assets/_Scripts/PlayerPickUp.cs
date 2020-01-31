using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private bool pickedUp = false;
    GameObject pickedUpGameobject;
    [SerializeField] Transform pickUpPosition;

    [SerializeField] float pickUpSpeed;

    [SerializeField] float sphereRadius;
    [SerializeField] LayerMask collideWithPickUp;
    GameObject closestObject;
    [SerializeField] bool secondPlayer = false;

    void Update()
    {
        Collider[] sphereHits = Physics.OverlapSphere(transform.position, sphereRadius, collideWithPickUp);

        Vector3 newTransform = transform.position;
        float destination = Mathf.Infinity;

        foreach(Collider r in sphereHits) // Find The Closest Object & Set It Into closestObject Gameobject
        {
            Vector3 diff = r.transform.position - newTransform;
            float newDistance = diff.sqrMagnitude;

            if(destination > newDistance && newDistance >= 0)
            {
                destination = newDistance;
                closestObject = r.transform.gameObject;
            }
        }

        if(closestObject != null) //We have found our closestObject
        {
            if(Time.time % 2  == 0)
            {
                Debug.Log("CLOSEST OBJECT IS " + closestObject.name);
            }

            string inputName = secondPlayer == false ? "PickUp1" : "PickUp2";
            if(Input.GetButtonDown(inputName) && !pickedUp) //If we have nothing in hand, pick up
            {
                pickedUpGameobject = closestObject;
                pickedUpGameobject.GetComponent<CapsuleCollider>().isTrigger = true;
                pickedUpGameobject.GetComponent<Rigidbody>().isKinematic = true;
                pickedUp = true;
            }
            else if(Input.GetButtonDown(inputName) && pickedUp) //If we have something in hand, throw
            {
                pickedUpGameobject.GetComponent<Rigidbody>().isKinematic = false;
                pickedUpGameobject.GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);

                pickedUpGameobject.GetComponent<CapsuleCollider>().isTrigger = false;
                pickedUp = false;
                pickedUpGameobject = null;
            }
        }

        if (pickedUp)
        {
            pickedUpGameobject.transform.position = Vector3.MoveTowards(pickedUpGameobject.transform.position, pickUpPosition.position, Time.deltaTime * pickUpSpeed);
            pickedUpGameobject.transform.rotation = Quaternion.RotateTowards(pickedUpGameobject.transform.rotation, pickUpPosition.rotation, Time.deltaTime * pickUpSpeed);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}