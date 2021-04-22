using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonSpawner : MonoBehaviour
{
    // whether or not ions should be spawned (for testing purposes).
    public bool on;

    // a prefab of an Ion to be instantiated. Maybe we make this an array so that we can choose which one?
    public Ion prefabIon;

    // counter used in `Update` to stall time between instantiations. Maybe switch to a WaitForSeconds system later...
    private int counter;

    // controls the frequency of instantiation. longer number = longer wait (pretty sure this should be "period", but `freq` sounds nicer)
    public int freq;

    // the starting x-coordinate of the Ion. ~ -10ish seems to work well for now
    public float xStart;

    // This will eventually be imported from a document ??
    private List<string> names;

    private void Start()
    {
        names = GetNames("Na");
        counter = 0;
    }

    /* instantiates an ion with a randomish position.
     */
    private void CreateIon(bool doYDiff = true)
    {
        float yDiff = 0f;
        if (doYDiff)
        {
            yDiff = Random.RandomRange(-1f, 1f);
        }
        Vector2 iPos = new Vector2(xStart, 3 * yDiff);

        Ion i = Instantiate(prefabIon, iPos, Quaternion.Euler(0, 0, 0));
        i.Setup(names[0], names[Random.Range(0, names.Count)], startMovement: true);
    }

    private void FixedUpdate()
    {
        if (on && counter > freq)
        {
            CreateIon();

            counter = 0;
        }

        counter++;
    }

    private List<string> GetNames(string s) {

        switch (s) {
            case "Na":
                return new List<string> { "Na<sup>+</sup>", "Na<sup>-</sup>", "Na<sup>2+</sup>" };// <sub>subscript text</sub>
            default:
                return new List<string> {"L"};
        }

    }
}
