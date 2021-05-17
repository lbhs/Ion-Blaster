using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    string score;
    public Text t;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("ScorePackage");
        Debug.Log(go);
        score = go.transform.position.x.ToString();
        t.text = score;
    }

    public void Restart()
    {
        Destroy(go);
        SceneManager.LoadScene(0);
    }
}
