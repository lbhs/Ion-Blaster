/* 
 * IonSpawner.cs
 * Gavin Gee
 *
 * This script handles the spawning of ions in the playable scenes as well as
 * the ions spawned in the loading scenes.
 * 
 * Below I have defined a couple of things implemented in this script that might not
 * be clear at a glance:
 * 
 * - An Ion's code is equal to the line number containing its name in
 *   `Assets/Scripts/Resources/Ion\ paths.txt` minus 1
 *   (e.g.: Sodium is on line 13, so it's code is 12.)
 * 
 * - `Setup` does not start the spawning of ions, `Activate` does. However, 
 *   starting the ion spawning without calling `Setup` will cause errors.
 *   
 * - While this script does choose where an ion is spawned, it does not handle
 *   the movement of the ion or its destination in any way. All of the ion's 
 *   movement is done in `Ion.cs`
 *   
 * - `allNames` is a list of the lists of possible labels for all ions in the
 *   scene. `currentNames` is a list of possible labels for just the current ion
 *   about to be spawned.
 *   
 * - The prefabs are all stored in Assets/Resources, because in order for a prefab
 *   to be loaded via `Resources.Load` as seen in `GetPrefabs`, it must be in a
 *   folder titled "Resources". The same was done for the "Ions.txt" and 
 *   "Ion paths.txt" folder, though this may not be necessary.
 *   
 * - The `Correct Sound` and `Incorrect Sound` audiosources that are on the ion
 *   prefabs are currently unused, but they have been left in case they become
 *   useful later.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonSpawner : MonoBehaviour
{
    public static IonSpawner main;

    // whether or not ions should be spawned (for testing purposes).
    private bool on = false;

    // a prefab of an Ion to be instantiated. Maybe we make this an array so that we can choose which one?
    private GameObject currentPrefab;

    public List<GameObject> prefabs;

    // counter used in `Update` to stall time between instantiations. Maybe switch to a WaitForSeconds system later...
    private int frameCounter;

    // number of frames between each ion instantiation. longer number = longer wait (pretty sure this should be `period`, but `freq` sounds nicer)
    public int freq;

    // the starting x-coordinate of the Ion. ~ -10ish seems to work well for current cam setup ...
    public float xStart;

    // A list of all possible labels that an Ion can be named given user's options. Generated from 'Assets/ions.txt'
    private List<List<string>> allNames;

    // A list of all possible labels that a single ion can be given (i.e.: { Na+, Na-, Na2+ }).
    private List<string> currentNames;

    // this int is used to iterate through both `prefabs` and `allNames`
    private int currentIterator = 0;

    // path to 'ions.txt', a db that contains all ion labels.
    public TextAsset labelsFile;
    // path to 'ion paths.txt', a db that contains all paths to ion prefabs.
    public TextAsset prefabsFile;

    // SEP is the character separator used in `Listify` to separate the string into a list.
    readonly char SEP = ',';

    // This is a list of codes corresponding to the ions chosen by the player.
    private List<int> ionCodes;

    // This list holds all of the ions spawned in the loading/pause screens.
    private List<GameObject> introIons;

    private void Start()
    {
        main = this;
    }

    /* This function initializes the ion spawner by doing the following:
     * 1. Populates `ionCodees` from `SpawnerHelper.cs` through `GetIonCodes`
     * 2. Gets all possible labels of all possible ions based on `ionCodes`
     * 3. Gets all prefabs associated with the ions in `ionCodes`
     * 4. Initializes a few values. (frameCounter, currentNames, currentPrefab, freq)
     * 5. `Debug.Log`s the whole thing.
     * 
     * The Logs have all been commented out for the final build by the 2021 ACR
     * team, but if you find yourself with issues in the IonSpawner.cs script,
     * it is reccomended that you uncomment these to find out where the problem
     * lies.
     */
    public void Setup(int _freq = 70)
    {
        //Debug.Log("[Setup]: Getting Ion Codes");
        GetIonCodes();
        //Debug.Log("[Setup]: Done.");

        //Debug.Log("[Setup]: Getting Possible Ion Names");
        GetNames();
        //Debug.Log("[IonSpawner.cs]: Done.");

        //Debug.Log("[IonSpawner.cs]: Loading Possible Prefabs");
        GetPrefabs();
        //Debug.Log("[IonSpawner.cs]: Done.");

        frameCounter = 0;
        currentNames = allNames[0];
        currentPrefab = prefabs[currentIterator];
        freq = _freq;

        //Debug.Log("[IonSpawner.cs]: Done Initializing.");
    }

    // this function activates the spawning of the ions. 
    public void Activate()
    {
        on = true;
    }

    // This function spawns the ions in the pause menu between playable scenes.
    public void SpawnIntroIons()
    {
        introIons = new List<GameObject>();
        switch (ionCodes.Count)
        {
            case 1:
                GameObject i1 = Instantiate(currentPrefab, Vector3.down, Quaternion.Euler(0, 0, 0));
                i1.GetComponent<Ion>().Setup(currentNames[0], currentNames[0], startMovement: false);
                introIons.Add(i1);
                break;
            case 2:
                GameObject i2 = Instantiate(currentPrefab, Vector3.down + Vector3.left * 3, Quaternion.Euler(0, 0, 0));
                i2.GetComponent<Ion>().Setup(currentNames[0], currentNames[0], startMovement: false);
                introIons.Add(i2);
                currentNames = allNames[1];
                currentPrefab = prefabs[1];
    
                GameObject i3 = Instantiate(currentPrefab, Vector3.down + Vector3.right * 3, Quaternion.Euler(0, 0, 0));
                i3.GetComponent<Ion>().Setup(currentNames[0], currentNames[0], startMovement: false);
                introIons.Add(i3);
                currentNames = allNames[0];
                currentPrefab = prefabs[0];
                break;
            case 3:
                GameObject i4 = Instantiate(currentPrefab, Vector3.down + Vector3.left * 5, Quaternion.Euler(0, 0, 0));
                i4.GetComponent<Ion>().Setup(currentNames[0], currentNames[0], startMovement: false);
                introIons.Add(i4);
                currentNames = allNames[1];
                currentPrefab = prefabs[1];

                GameObject i5 = Instantiate(currentPrefab, Vector3.down, Quaternion.Euler(0, 0, 0));
                i5.GetComponent<Ion>().Setup(currentNames[0], currentNames[0], startMovement: false);
                introIons.Add(i5);
                currentNames = allNames[2];
                currentPrefab = prefabs[2];

                GameObject i6 = Instantiate(currentPrefab, Vector3.down + Vector3.right * 5, Quaternion.Euler(0, 0, 0));
                i6.GetComponent<Ion>().Setup(currentNames[0], currentNames[0], startMovement: false);
                introIons.Add(i6);
                currentNames = allNames[0];
                currentPrefab = prefabs[0];
                break;
            default:
                return;
        }
    }

    // This function removes the ions created by `SpawnIntroIons`.
    public void DestroyIntroIons()
    {
        while (introIons.Count != 0)
        {
            GameObject i = introIons[0];
            introIons.Remove(i);
            Destroy(i);
        }
    }

    /* This function contains the main loop that the spawner operates on.
     * The time that ions are spawned at are seperated by `freq` # of frames.
     */
    private void FixedUpdate()
    {
        if (on && frameCounter > freq)
        {
            currentPrefab = prefabs[currentIterator];
            currentNames = allNames[currentIterator];

            CreateIon();
            frameCounter = 0;

            currentIterator++;

            if (currentIterator >= allNames.Count)
            {
                currentIterator = 0;
            }
        }

        frameCounter++;
    }

    /* This function spawns an Ion prefab with a random position off the right
     * of the screen and sets up the ion class on the prefab.
     */
    private void CreateIon(bool doYDiff = true)
    {
        float yDiff = 0f;
        if (doYDiff)
        {
            yDiff = Random.Range(-1f, 1f);
        }
        Vector2 iPos = new Vector2(xStart, 3 * yDiff);

        GameObject i = Instantiate(currentPrefab, iPos, Quaternion.Euler(0, 0, 0));

        i.GetComponent<Ion>().Setup(currentNames[0], currentNames[Random.Range(0, currentNames.Count)], startMovement: true); // SORRY
    }


    /* This function will populate `ionCodes` depending on what the user selected
     * on the title screen by referencing the `SpawnerHelper` object.
     */
    private void GetIonCodes()
    {
        if (SpawnerHelper.main)
        {
            ionCodes = SpawnerHelper.main.GetIonCodes();
            return;
        }
        ionCodes = new List<int> { 12, 16 };
    }


    /* This function populates `allNames`, the list of possible ion labels,
     * based on the contents of `ionCodes`
     *
     * This function relies on the helper function `Listify`, which turns a string
     * into a list.
     */
    private void GetNames() {

        string[] allLabels = labelsFile.text.Split('\n'); 

        allNames = new List<List<string>>();
        foreach (int i in ionCodes)
        {
            allNames.Add(Listify(allLabels[i]));
        }
    }

    // This function populates `prefabs` based on the contents of ionCodes
    private void GetPrefabs()
    {
        string[] allPaths = prefabsFile.text.Split('\n');

        prefabs = new List<GameObject>();
        foreach (int i in ionCodes)
        {
            Debug.Log("[IonSpawner.cs]: " + allPaths[i]);
            prefabs.Add(Resources.Load(allPaths[i], typeof(GameObject)) as GameObject);
            Debug.Log(prefabs[0]);
        }
    }

    /* This function takes in a string and turns it into a list of strings, seperated 
     * by `SEP`
     */
    private List<string> Listify(string str)
    {
        List<string> l = new List<string> { };
        string[] arr = str.Split(SEP);

        foreach (string substr in arr)
        {
            l.Add(substr);
        }

        return l;
    }
}
