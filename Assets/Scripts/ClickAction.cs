﻿using System.Collections;
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
                Debug.Log(rhInfo.collider.name); // 'name' can be substituted out for the name of the ion instead of the name of the gameObject

                Destroyable destroyableScript = rhInfo.collider.GetComponent<Destroyable>();
                if(destroyableScript)
                {
                    destroyableScript.RemoveMe();
                }
            }

            else
            {
                Debug.Log("incorrect answer");
            }
        }
    }
}