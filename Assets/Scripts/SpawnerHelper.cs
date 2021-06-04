/* 
 * SpawnerHelper.cs
 * Gavin Gee
 * 
 * This script handles the selection of ions on the ion selection screen. It also
 * holds onto which ions were selected for usage later by `IonSpawner.cs`.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHelper : MonoBehaviour
{
    public static SpawnerHelper main;

    public DropdownHandler dh1; // the leftmost dropdown in the ion selection screen
    public DropdownHandler dh2; // the middle dropdown in the ion selection screen
    public DropdownHandler dh3; // the rightmost dropdown in the ion selection screen
    private List<int> ionCodes; // a list of ion codes to be used by `IonSpawner.cs`

    void Start()
    {
        ionCodes = new List<int> { 12, 16 }; // Default ion codes = sodium (line 13) chloride (line 17)
        main = this;
        if (!FindObjectOfType<IonSpawner>())
        {
            DontDestroyOnLoad(this);
        }
    }

    // This function just returns the ion codes list to make it accessible by `IonSpawner.cs`
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
        }
    }

    /* This function randomizes which ion codes should be used by the ion spawner
     * in the next scene. It will always choose 1 metal ion and 1 other ion (either
     * non-metal or polyatomic).
     */
    public void Randomize()
    {
        Debug.Log("Randomizing ...");

        ionCodes = new List<int>();
        int i1 = Random.Range(1, 14);
        int i2 = Random.Range(15, 31);
        ionCodes.Add(i1);
        ionCodes.Add(i2);

        Debug.Log(ionCodes);
    }
}
