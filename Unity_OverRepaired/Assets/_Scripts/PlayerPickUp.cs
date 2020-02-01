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
    [SerializeField] int pickdUpLayer, droppedLayer;
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
                //closestObject = r.transform.gameObject;

                  
                closestObject = r.transform.gameObject;

            }
        }

        if(closestObject != null) //We have found our closestObject
        {
            string inputName = secondPlayer == false ? "PickUp1" : "PickUp2";
            if(Input.GetButtonDown(inputName) && !pickedUp) //If we have nothing in hand, pick up
            {
                if (closestObject.gameObject.layer == 12)
                {
                    pickedUpGameobject = closestObject?.GetComponent<BrokenRobot>()?.getPart()?.gameObject;
                }
                else
                {
                    pickedUpGameobject = closestObject;
                }
                if (pickedUpGameobject != null) {
                    pickedUpGameobject.GetComponent<Collider>().isTrigger = true;
                    pickedUpGameobject.GetComponent<Rigidbody>().isKinematic = true;
                    pickedUpGameobject.layer = pickdUpLayer;
                    pickedUp = true;
                }
            }
            else if(Input.GetButtonDown(inputName) && pickedUp) //If we have something in hand, throw
            {
                pickedUpGameobject.GetComponent<Rigidbody>().isKinematic = false;
                pickedUpGameobject.GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);

                pickedUpGameobject.GetComponent<Collider>().isTrigger = false;
                pickedUpGameobject.layer = droppedLayer;

                pickedUp = false;
                pickedUpGameobject = null;
                closestObject = null;
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