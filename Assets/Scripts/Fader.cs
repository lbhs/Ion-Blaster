using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public Image img;
    private int sceneIndex;
    public AudioSource AS;

    public Text timer;
    public Text bottomText;

    private void Start()
    {
        //img = GameObject.Find("Black Screen").GetComponent<Image>();
        img.enabled = true;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (!img)
        {
            return;
        }

        if (sceneIndex < 2)
        {
            StartCoroutine(DefaultFadeIn());
        } else if (sceneIndex < 7)
        {
            StartCoroutine(GameSceneFadeIn());
        }
    }

    private IEnumerator DefaultFadeIn()
    {
        Debug.Log("Fading in...");
        img.CrossFadeAlpha(0, .75f, false);
        yield return new WaitForSeconds(.75f);
    }

    public void FadeSceneChange()
    {
        StartCoroutine(DefaultFadeOut());
    }

    private IEnumerator DefaultFadeOut()
    {
        Debug.Log("Fading out...");
        img.CrossFadeAlpha(1, .75f, false);
        yield return new WaitForSeconds(.75f);
        SceneManager.LoadScene( sceneIndex + 1);
    }

    // call ionspawner.main.setup somewhere in here
    public IEnumerator GameSceneFadeIn()
    {
        IonSpawner.main.Setup();
        yield return new WaitForSeconds(1f);

        bottomText.text = "You are looking for these ions";
        for (int i = 3; i > 0; i--)
        {
            timer.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        bottomText.text = "";
        IonSpawner.main.DestroyIntroIons();

        timer.text = "Go!";
        AS.Play();

        img.CrossFadeAlpha(0, .5f, false);
        yield return new WaitForSeconds(.5f);

        timer.text = "";

        GameFlowController.main.BeginGame();
    }
}
