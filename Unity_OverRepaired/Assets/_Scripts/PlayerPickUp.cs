using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private bool pickedUp = false;
    private bool interact = false;
    GameObject pickedUpGameobject;
    GameObject interactObject;
    [SerializeField] Transform pickUpPosition;

    [SerializeField] float pickUpSpeed;

    [SerializeField] float sphereRadius;
    [SerializeField] LayerMask collideWithPickUp, collideWithWorkBench;
    GameObject closestObject;
    [SerializeField] int pickdUpLayer, droppedLayer;
    [SerializeField] bool secondPlayer = false;

    bool findClosestFirst = false;

    private PlayerMovement pM;

    Vector3 finalPosition;

    void Start()
    {
        pM = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Collider[] pickUpHits = Physics.OverlapSphere(transform.position, sphereRadius, collideWithPickUp);
        Collider[] workBenchHits = Physics.OverlapSphere(transform.position, sphereRadius, collideWithWorkBench);

        string interactName = !secondPlayer ? "Interact1" : "Interact2";
        string pickUpName = !secondPlayer ? "PickUp1" : "PickUp2";

        finalPosition = pickUpPosition.position;

        if (Input.GetButton(interactName) && pickedUp)
        {
            if (!findClosestFirst)
            {
                var closest = GetClosest(workBenchHits);
                interactObject = closest?.gameObject;
                findClosestFirst = true;
            }

            if(interactObject != null && interactObject.layer == 12)
            {
                if(interactObject.GetComponent<BrokenRobot>().setPart(pickedUpGameobject.GetComponent<RobotPart>()))
                {
                    pickedUp = false;
                    pickedUpGameobject = null;
                    interactObject = null;
                    findClosestFirst = false;
                    interact = false;
                }
            }
            else
            {
                if (interactObject == null)
                {
                    DropObject(pickedUpGameobject);
                    pickedUp = false;
                    pickedUpGameobject = null;
                }
                else
                {
                    interact = true;

                    if (interactObject.GetComponent<RepairStation>().work(pickedUpGameobject.GetComponent<RobotPart>()))
                    {
                        finalPosition = interactObject.transform.GetChild(0).position;
                        pM.StopMovement();
                    }
                    else
                    {
                        pM.ContinueMovement();
                    }
                }
            }
        }
        else if(Input.GetButtonUp(interactName))    
        {
            interactObject?.GetComponent<RepairStation>()?.resetWork();

            interact = false;
            interactObject = null;
            findClosestFirst = false;

            pM.ContinueMovement();
        }

        if (Input.GetButtonDown(pickUpName) && !interact)
        {
            if(!pickedUp)
            {
                var closest = GetClosest(pickUpHits);

                pickedUpGameobject = closest?.gameObject.layer == 12 ? closest?.GetComponent<BrokenRobot>()?.getPart()?.gameObject : closest?.gameObject;

                if (pickedUpGameobject != null)
                {
                    PickUpObject(pickedUpGameobject);
                    pickedUp = true;
                }
            }
            else if(pickedUp)
            {
                ThrowObject(pickedUpGameobject);
                pickedUp = false;
                pickedUpGameobject = null;
            }
        }


        if(interact)
        {
            Debug.Log("INTERACTING WOKRING");
            
        }
        if (pickedUp)
        {
            pickedUpGameobject.transform.position = Vector3.MoveTowards(pickedUpGameobject.transform.position, finalPosition, Time.deltaTime * pickUpSpeed);
            pickedUpGameobject.transform.rotation = Quaternion.RotateTowards(pickedUpGameobject.transform.rotation, pickUpPosition.rotation, Time.deltaTime * pickUpSpeed);
        }
    }

    void PickUpObject(GameObject objectToPickUp)
    {
        objectToPickUp.GetComponent<Collider>().isTrigger = true;
        objectToPickUp.GetComponent<Rigidbody>().isKinematic = true;
        objectToPickUp.layer = pickdUpLayer;
    }
    void ThrowObject(GameObject objectToThrow)
    {
        objectToThrow.GetComponent<Rigidbody>().isKinematic = false;
        objectToThrow.GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);

        objectToThrow.GetComponent<Collider>().isTrigger = false;
        objectToThrow.layer = droppedLayer;
    }

    void DropObject(GameObject objectToDrop)
    {
        objectToDrop.GetComponent<Rigidbody>().isKinematic = false;
        objectToDrop.GetComponent<Collider>().isTrigger = false;
        objectToDrop.layer = droppedLayer;
    }

    GameObject GetClosest(Collider[] sphereHits)
    {
        Vector3 newTransform = transform.position;
        float destination = Mathf.Infinity;
        GameObject closest = null;

        foreach (Collider r in sphereHits) // Find The Closest Object & Set It Into closestObject Gameobject
        {
            Vector3 diff = r.transform.position - newTransform;
            float newDistance = diff.sqrMagnitude;

            if (destination > newDistance && newDistance >= 0)
            {
                destination = newDistance;
                closest = r.transform.gameObject;
            }
        }

        return closest;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}