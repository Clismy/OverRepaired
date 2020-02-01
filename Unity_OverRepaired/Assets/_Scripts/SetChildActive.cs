using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildActive : MonoBehaviour
{
    public GameObject child;

    public void setTrue()
    {
        child.SetActive(true);
    }

}
