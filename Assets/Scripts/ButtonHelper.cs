using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour
{
    public Button b;
    public DropdownHandler dh1;
    public DropdownHandler dh2;
    public DropdownHandler dh3;

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
