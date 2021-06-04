using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ion : MonoBehaviour
{
    private string m_vFormula; // the formula visible to the player.
    private string m_tFormula; // the correct formula for this ion.

    private float m_xBound; // how far in the x direction the ion can reach before being destroyed.

    private bool moving = false;
    private Vector3 dir; // the end position of the ion.
    private Rigidbody rb;

    private float speed;
    public GameObject text;
    private Transform canvas;

    /* Eventually, I'd like this constructor-y thing to take in just the true formula
     * and randomly choose the visible formula from a list of possible names.
     * For now, it will have parameters for both `vFormula` and `tFormula`.
     */
    public void Setup(string tFormula, string vFormula, bool startMovement = true, bool hasText = true, float xBound = 9.5f)
    {
        m_tFormula = tFormula;
        m_vFormula = vFormula;

        gameObject.name = m_tFormula;

        if (startMovement) StartMovement();
        if (hasText) SetupText();

        m_xBound = xBound;
    }

    /* This function begins the movement of the ion and defines where it should
     * move towards (`end`).
     */
    public void StartMovement()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 end = new Vector3(10, 3 * Random.Range(-1f, 1f), 0);
        dir = end - transform.position;
        moving = true;
    }

    /* This function sets up the TextMeshPro object associated with this script's
     * gameobject.
     */
    private void SetupText()
    {
        TextMeshPro t = gameObject.GetComponentInChildren<TextMeshPro>();
        t.text = m_vFormula;
    }

    /* This script returns true if the ion has a correct formula and false if the
     * formula is incorrect.
     */
    public bool CheckFormula()
    {
        if (m_vFormula == m_tFormula)
        {
            return true;
        }

        return false;
    }

    public void FixedUpdate()
    {
        if (moving)
        {
            Vector3 tempVect = dir.normalized * 4.5f * Time.deltaTime;
            rb.MovePosition(transform.position + tempVect);

            if (rb.position.x > m_xBound)
            {
                DestroyIon();
            }
        }
    }

    /* This function destroys this script, the gameobject associated with it,
     * and removes one life from the player.
     */
    private void DestroyIon()
    {
        Debug.Log("Destroying a " + m_tFormula + " ion.");
        if (CheckFormula() && GameFlowController.main)
        {
            GameFlowController.main.EditLife(-1);
        } 

        Destroy(gameObject);
    }
}
