/* 
 * ButtonHelper.cs
 * Gavin Gee
 * 
 * This script enables and disables the start button in the ion selection screen
 * depending on whether or not the player has selected an option from one of
 * the three dropdowns.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour
{
    public Button b; // the start button
    public DropdownHandler dh1; // the leftmost dropdown
    public DropdownHandler dh2; // the middle dropdown
    public DropdownHandler dh3; // the rightmost dropdown

    // Start is called before the first frame update
    void Start()
    {
        b.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dh1.GetSelection() != 0 || dh2.GetSelection() != 0 || dh3.GetSelection() != 0)
        {
            b.interactable = true;
        }
        else
        {
            b.interactable = false;
        }
    }
}
