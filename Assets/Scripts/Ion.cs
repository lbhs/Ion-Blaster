using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ion : MonoBehaviour
{
    private string m_vFormula; // the formula visible to the player.
    private string m_tFormula; // the correct formula for this ion.

    private bool moving = false;
    private Vector3 dir; // the end position of the ion.
    private Rigidbody rb;

    public AudioSource SoundCorrect;
    public AudioSource SoundIncorrect;

    /* Eventually, I'd like this constructor-y thing to take in just the true formula
     * and randomly choose the visible formula from a list of possible names.
     * For now, it will have parameters for both `vFormula` and `tFormula`.
     */
    public void Setup(string tFormula, bool startMovement = false)
    {
        m_tFormula = tFormula;
        m_vFormula = m_tFormula;

        gameObject.name = m_tFormula;

        if (startMovement) StartMovement();
    }

    public void StartMovement()
    {
        Debug.Log("movement starting...");
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 end = new Vector3(10, 3 * Random.RandomRange(-1f, 1f), 0);
        dir = end - transform.position;
        moving = true;
    }

    bool CheckFormula()
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

            if (rb.position.x > 12)
            {
                DestroyIon();
            }
        } 
    }

    private void DestroyIon()
    {
        Debug.Log("Destroying a " + m_tFormula + " ion.");
        Destroy(gameObject);
    }
}
