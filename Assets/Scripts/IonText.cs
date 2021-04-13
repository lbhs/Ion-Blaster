/*
 * IonText.cs
 * Gavin
 *
 * This script controls the rotation of the text around the ion. Controlled
 * by the boolean "active" at the top of the class. Active by default. Can be
 * turned off here or in the inspector (wouldn't recommend the inspector route
 * though, just added it in case it was convenient for debugging).
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonText : MonoBehaviour
{
    public bool active = true;

    Vector3 xAxisDir;
    Vector3 yAxisDir;
    Vector3 zAxisDir;

    Vector3 ionPos;

    float speed; // degrees / sec

    private void Start()
    {
        xAxisDir = (1 - 2 * Random.Range(0, 2)) * Vector3.right;
        yAxisDir = (1 - 2 * Random.Range(0, 2)) * Vector3.up;
        zAxisDir = (1 - 2 * Random.Range(0, 2)) * Vector3.forward; // TODO: make this less gross

        speed = Random.Range(10, 21);

        ionPos = transform.parent.position;
        Debug.Log(transform.parent.name);
    }

    void FixedUpdate()
    {
        if (!active) return;
        transform.RotateAround(ionPos, xAxisDir, speed * Time.deltaTime);
        transform.RotateAround(ionPos, yAxisDir, speed * Time.deltaTime);
        transform.RotateAround(ionPos, zAxisDir, speed * Time.deltaTime);
    }
}
