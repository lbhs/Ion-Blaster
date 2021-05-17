using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHelper : MonoBehaviour
{
    public static SpawnerHelper main;
    public DropdownHandler dh1;
    public DropdownHandler dh2;
    public DropdownHandler dh3;
    private List<int> ionCodes; 
    // Start is called before the first frame update
    void Start()
    {
        ionCodes = new List<int> { 12, 16 }; // Default sol'n = sodium (11) chloride (16)
        main = this;
        if (!FindObjectOfType<IonSpawner>())
        {
            DontDestroyOnLoad(this);
        }
    }

    public List<int> GetIonCodes()
    {
        return ionCodes;
    }

    /* The following function sets which ion codes should be used by the ion
     * spawner in the next scene. This should be called by a button.
     */
    public void SetIonCodes()
    {
        if (dh1)
        {
            int i1 = dh1.GetSelection();
            int i2 = dh2.GetSelection();
            int i3 = dh3.GetSelection();
            if (i1 + i2 + i3 != 0)
            {
                ionCodes = new List<int>();
            }
            if (i1 > 0) ionCodes.Add(i1);
            if (i2 > 0) ionCodes.Add(i2 + 14);
            if (i3 > 0) ionCodes.Add(i3 + 22);
            //ionCodes = new List<int>() { dh1.GetSelection(), dh2.GetSelection() + 14};
        }
    }
}
