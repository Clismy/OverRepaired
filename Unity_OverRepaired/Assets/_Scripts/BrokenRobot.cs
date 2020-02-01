using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrokenRobot : MonoBehaviour
{
    public RobotPart[] parts = new RobotPart[6];
    public float speed = 0.1f;
    [Range (1,6)]
    public int howManyBrokenParts;


    public void Start()
    {
        for (int i = 0; i < 6; i++) {
           var temp = Instantiate(parts[i], transform.GetChild(i));
           temp.gameObject.layer = 0;
        }
        var brokenParts = randomBroken();
        for (int y = 0; y < howManyBrokenParts; y++)
        {
            transform.GetChild(brokenParts[y]).GetChild(0).GetComponent<RobotPart>().isBroken = true;
        }
    }

    public void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

    }

    public int[] randomBroken()
    {
        int[] list = new[] { 1,2,3,4,5};
        return list.OrderBy(x => Random.Range(0, parts.Length)).Take(howManyBrokenParts).ToArray();
    }

    public RobotPart getPart()
    {
        for (int i = 0; i < 6; i++)
        {
            var temp = transform.GetChild(i).childCount;
            if (temp > 0){
                var tempRobotPart = transform.GetChild(i).GetChild(0).GetComponent<RobotPart>();
                if (tempRobotPart != null && tempRobotPart.isBroken)
                {
                    tempRobotPart.transform.parent = null;
                    tempRobotPart.gameObject.layer = 11;
                    return tempRobotPart;
                }
            }
        }
        return null;
    }

    public void setPart(RobotPart part)
    {
        if (!part.isBroken && transform.GetChild((int)part.whatPart).childCount > 0 )
        {
            part.transform.parent = transform.GetChild((int)part.whatPart);
        }
    }

}
