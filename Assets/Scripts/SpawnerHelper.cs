using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHelper : MonoBehaviour
{
    public static SpawnerHelper main;
    public DropdownHandler dh1;
    public DropdownHandler dh2;
    private List<int> ionCodes; 
    // Start is called before the first frame update
    void Start()
    {
        ionCodes = new List<int> { 11, 15 }; // Default sol'n = sodium (11) chloride (16)
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
        if (dh1 && dh2)
        {
            if (dh1.GetSelection() == 0 || dh2.GetSelection() == 14)
            {
                return;
            }
            ionCodes = new List<int>() { dh1.GetSelection(), dh2.GetSelection() + 14 };
        }
    }
}
