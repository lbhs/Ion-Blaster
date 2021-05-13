using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ion : MonoBehaviour
{
    private string m_vFormula; // the formula visible to the player.
    private string m_tFormula; // the correct formula for this ion.

    private float m_xBound;

    private bool moving = false;
    private Vector3 dir; // the end position of the ion.
    private Rigidbody rb;

    public AudioSource SoundCorrect;
    public AudioSource SoundIncorrect;

    private float speed;
    public GameObject text;
    private Transform canvas;

    /* Eventually, I'd like this constructor-y thing to take in just the true formula
     * and randomly choose the visible formula from a list of possible names.
     * For now, it will have parameters for both `vFormula` and `tFormula`.
     */
    public void Setup(string tFormula, string vFormula, bool startMovement = true, bool hasText = true, float xBound = 12)
    {
        m_tFormula = tFormula;
        m_vFormula = vFormula;

        gameObject.name = m_tFormula;

        if (startMovement) StartMovement();
        if (hasText) SetupText();

        m_xBound = xBound;
    }

    public void StartMovement()
    {
        // Debug.Log("movement starting...");
        rb = gameObject.GetComponent<Rigidbody>();
        // Debug.Log(rb);
        Vector3 end = new Vector3(10, 3 * Random.RandomRange(-1f, 1f), 0);
        dir = end - transform.position;
        moving = true;
    }

    private void SetupText()
    {
        TextMeshPro t = gameObject.GetComponentInChildren<TextMeshPro>();
        t.text = m_vFormula;
    }

    public bool CheckFormula()
    {
        if (m_vFormula == m_tFormula)
        {
            SoundCorrect.Play(); return true;
        }
        return false;

    }

    void DebugPrintFormulas()
    {
        if (CheckFormula())
        {
            Debug.Log("This is a " + m_tFormula + " ion, but it apears to the player as `" + m_vFormula + "`");
            SoundIncorrect.Play();
        }
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
