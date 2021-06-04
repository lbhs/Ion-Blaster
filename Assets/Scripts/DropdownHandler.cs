/*
 * DropdownHandler.cs
 * Liela Andringa
 * 
 * This script operates the dropdowns in the Ion Selection scene. The function
 * `GetSelection` can be called to return which ion was chosen by the player (as
 * an integer ion code).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    // dropdown box to populate with ion images
    public TMP_Dropdown dropDown;

    // the ion images representing the ion choices
    public Sprite[] ions;


    void Start()
    {
        dropDown.ClearOptions();

        List<TMP_Dropdown.OptionData> ionItems = new List<TMP_Dropdown.OptionData>();

        foreach (var ion in ions)
        {
            var ionOption = new TMP_Dropdown.OptionData(ion.name, ion);
            ionItems.Add(ionOption);
        }

        dropDown.AddOptions(ionItems);

    }

    public int GetSelection()
    {
        return dropDown.value;
    }

}
