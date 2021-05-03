using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonSpawner : MonoBehaviour
{
    // whether or not ions should be spawned (for testing purposes).
    public bool on = true;

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

    private List<string> currentNames;

    private int currentIterator = 0;

    // path to 'ions.txt', a db that contains all ion labels.
    readonly static string LABELS_PATH = @"Assets/Scripts/Resources/Ions.txt";
    public TextAsset labelsFile;
    public TextAsset prefabsFile;

    // SEP is the character separator used in `Listify` to separate the string into a list.
    readonly static char SEP = ',';

    private List<int> ionCodes;

    /* Called before frame 1 of `FixedUpdate`.
     * TODO: add a function that gets the ion prefabs... probably `GetIonPrefabs`.
     */
    private void Start()
    { 
        Debug.Log("[IonSpawner.cs]: Getting Ion Codes");
        GetIonCodes();
        Debug.Log("[IonSpawner.cs]: Done.");

        Debug.Log("[IonSpawner.cs]: Getting Possible Ion Names");
        GetNames();
        Debug.Log("[IonSpawner.cs]: Done.");

        Debug.Log("[IonSpawner.cs]: Loading Possible Prefabs");
        GetPrefabs();
        Debug.Log("[IonSpawner.cs]: Done.");

        frameCounter = 0;
        currentNames = allNames[0];

        Debug.Log("[IonSpawner.cs]: Done Initializing.");
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

    /* instantiates an ion with a randomish position.
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


    // The below seection of code relates to getting the labels to be used by the ion spawner
    // depending on which ions the user selects in the title screen.


    /* This function will populate `ionCodes` depending on what the user selected on the title screen.
     * Right now, the ions to be used are hard-coded. If you would like to change which ions are used,
     * put the proper ion codes into the list below. 
     * 
     * An Ion's code is equal to the line number containing it's name in 
     * Assets/Scripts/Resources/Ion\ paths.txt minus 1.
     * (e.g.: Sodium is on line 12, so it's code is 11.)
     */
    private void GetIonCodes()
    {
        ionCodes = new List<int> { 9, 15 };
    }


    /* This function takes in a string ID for the ion(s) (currently just one) that
     * were selected on the title screen, and returns a list of possible ion labels.
     *
     * This function relies on the helper function `Listify`, which turns a string
     * into a list.
     *
     */
    private void GetNames() {

        string[] allLabels = labelsFile.text.Split('\n'); //string[] allLabels = System.IO.File.ReadAllLines(LABELS_PATH); // { "Na<sup>+</sup>,Na<sup>-</sup>,Na<sup>2+</sup>" };

        allNames = new List<List<string>>();
        foreach (int i in ionCodes)
        {
            allNames.Add(Listify(allLabels[i])); // TODO: try/catch here ? 
        }
    }

    private void GetPrefabs()
    {
        string[] allPaths = prefabsFile.text.Split('\n');

        prefabs = new List<GameObject>();
        foreach (int i in ionCodes)
        {
            Debug.Log("[IonSpawner.cs]: " + allPaths[i]);
            prefabs.Add(Resources.Load(allPaths[i], typeof(GameObject)) as GameObject); //"Metals/Sodium"
            Debug.Log(prefabs[0]);
        }
    }

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
