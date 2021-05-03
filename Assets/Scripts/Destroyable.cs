using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public void RemoveMe()
    {
        //Debug.Log("Destroyable's RemoveMe function called on " + name);
        Destroy(gameObject);
    }
}
