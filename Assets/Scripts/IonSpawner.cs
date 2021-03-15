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

    private void Start()
    {
        counter = 0;
    }


    /* instantiates an Ion with the given "yDiff"
     */
    private void CreateIon(Ion ion, bool doYDiff = true) 
    {
        float yDiff = 0f;
        if (doYDiff)
        {
            yDiff = Random.RandomRange(-1f, 1f);
        }
        Vector2 ionPos = new Vector2(xStart, 3 * yDiff);
        
        Ion i = Instantiate(ion, ionPos, Quaternion.Euler(0, 0, 0));

        // TODO: find a better way to assign these later.
        i.formula = "Na^+";
        i.gameObject.name = "Sodium Ion";

        Rigidbody2D iRB = i.gameObject.GetComponent<Rigidbody2D>();
        float yVel = (-yDiff * Random.value) / 2;
        iRB.velocity = new Vector2(1, yVel); 
    }

    private void Update()
    {
        if (counter > freq)
        {
            CreateIon(prefabIon);

            counter = 0;
        }

        counter++;
    }
}
