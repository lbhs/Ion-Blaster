using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public Image img;

    private void Start()
    {
        //img = GameObject.Find("Black Screen").GetComponent<Image>();
        img.enabled = true;
        if (img)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("Fading ...");
        img.CrossFadeAlpha(0, .75f, false);
        yield return new WaitForSeconds(.75f);
    }

    public void FadeSceneChange()
    {
        StartCoroutine(SceneChangeHelper());
    }

    private IEnumerator SceneChangeHelper()
    {
        Debug.Log(img.color.a);
        img.CrossFadeAlpha(1, .75f, false);
        yield return new WaitForSeconds(.75f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
