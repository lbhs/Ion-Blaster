/* 
 * Fader.cs
 * Gavin Gee
 * 
 * This script handles the fade in and out of black at the beginning of each scene.
 * It is placed on the `Fader` object in each scene. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    // The `Black Screen` object in each scene.
    public Image img;
    // The build index for the current scene.
    private int sceneIndex;
    // The audiosource for the music in the game scene.
    public AudioSource AS;

    // This is the text object for the timer in the game scenes.
    public Text timer;

    private void Start()
    {
        //img = GameObject.Find("Black Screen").GetComponent<Image>();
        img.enabled = true;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (!img)
        {
            return;
        }
        if (sceneIndex == 1)
        {

        }

        if (sceneIndex < 2 || sceneIndex % 2 == 0 || sceneIndex == 13)
        {
            StartCoroutine(DefaultFadeIn());
        } else if (sceneIndex < 12)
        {
            StartCoroutine(GameSceneFadeIn());
        }
    }

    /* This function is used to fade into all non-playable scenes 
     * (title, ion selection, tutorial, etc.)
     */
    private IEnumerator DefaultFadeIn()
    {
        Debug.Log("Fading in...");
        img.CrossFadeAlpha(0, .75f, false);
        yield return new WaitForSeconds(.75f);
    }

    /* This function just allows `DefaultFadeOut` to be called by buttons or
     * other scripts.
     */
    public void FadeSceneChange()
    {
        StartCoroutine(DefaultFadeOut());
    }

    public void FadeToEnd()
    {
        StartCoroutine(DefaultFadeOut(12));
    }

    public void FadeToTutorial()
    {
        StartCoroutine(DefaultFadeOut(13));
    }

    public void FadeToTitle()
    {
        StartCoroutine(DefaultFadeOut(0));
    }

    /* This function fades the a scene out to black before loading the next scene.
     */
    private IEnumerator DefaultFadeOut(int i = 20)
    {
        Debug.Log("Fading out...");
        img.CrossFadeAlpha(1, .75f, false);
        yield return new WaitForSeconds(.75f);
        if (i == 20)
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(i);
        }
    }

    /* This function handles the fade in from black and timer before a playable
     * scene starts.
     * 
     * TODO: clean this up!
     */
    public IEnumerator GameSceneFadeIn()
    {
        yield return new WaitForSeconds(1f);
        //IonSpawner.main.Setup();

        for (int i = 3; i > 0; i--)
        {
            timer.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        //IonSpawner.main.DestroyIntroIons();

        timer.text = "Go!";
        AS.Play();
        //StartCoroutine(SpriteFade(s, 0, 0.5f));

        img.CrossFadeAlpha(0, .5f, false);
        yield return new WaitForSeconds(.5f);

        timer.text = "";

        GameFlowController.main.BeginGame();
    }
}
