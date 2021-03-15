using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ion : MonoBehaviour
{
    public string formula; // hide later

    bool CompareFormulaTo(string s)
    {
        return formula == s;
    }

    void DebugPrintFormula()
    {
        Debug.Log(formula);
    }
}
