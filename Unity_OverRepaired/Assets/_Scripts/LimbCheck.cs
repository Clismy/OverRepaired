using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LimbCheck : MonoBehaviour
{
    public List<GameObject> limbs;
    private List<GameObject> brokenLimbs = new List<GameObject>();
    public GameObject tsest;

    void CheckLimb()
    {
        brokenLimbs.Add(tsest);
        for (int i = 0; i < brokenLimbs.Count; i++)
        {
            for (int j = 0; j < limbs.Count; j++)
            {
                if (brokenLimbs[i].name == limbs[j].name)
                {
                    limbs[j].GetComponent<Image>().color = Color.red;
                    limbs[j].GetComponent<SetChildActive>().setTrue();
                }
            }
        }

    } 


    // Update is called once per frame
    void Update()
    {
        CheckLimb();
    }
}
