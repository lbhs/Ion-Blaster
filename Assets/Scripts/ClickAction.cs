/*
 * ClickAction.cs
 * Liela Andringa
 * 
 * This script handles the registry of the player clicking ions in the playable scenes. 
 * The actual destruction of the ion and awarding of points is handled by `Destroyable.cs`
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didHit = Physics.Raycast(toMouse, out rhInfo);

            if(didHit)
            {
                Destroyable destroyableScript = rhInfo.collider.GetComponent<Destroyable>();
                if(destroyableScript)
                {
                    destroyableScript.RemoveMe();
                }
            }
        }
    }
}
