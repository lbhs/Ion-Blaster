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
    // The audiosource for the bossa nova music in the game scene.
    public AudioSource AS;

    // This is the text object for the timer in the game scenes.
    public Text timer;

    private void Start()
    {
        img.enabled = true; // this enables the image. it is initially disabled so it isn't seen in the unity editor.
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // if there is no "black screen" image, the Fader object doesn't need to fade in/out
        if (!img)
        {
            return;
        }

        // if a scene is the title or ion selection scene (< 2), a loading screen (even scenes < 12), or the tutorial (13), fade in normally with `DefaultFadeIn`
        if (sceneIndex <= 2
            || sceneIndex % 2 == 0
            || sceneIndex == 13)
        {
            StartCoroutine(DefaultFadeIn());
        }
        // otherwise, fade in with the count down by using `GameSceneFadeIn`
        else if (sceneIndex < 12)
        {
            StartCoroutine(GameSceneFadeIn());
        }
    }

    /* This function is used to fade into all non-playable scenes 
     * (title, ion selection, tutorial, etc.)
     */
    private IEnumerator DefaultFadeIn()
    {
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

    // This function fades to the end card (12)
    public void FadeToEnd()
    {
        StartCoroutine(DefaultFadeOut(12));
    }

    // This function fades to the tutorial (13)
    public void FadeToTutorial()
    {
        StartCoroutine(DefaultFadeOut(13));
    }

    // This function fades to the title screen (0)
    public void FadeToTitle()
    {
        StartCoroutine(DefaultFadeOut(0));
    }

    /* This function fades the scene out to black before loading the next scene.
     * The int `i` is an overload argument that can be used to fade to a specific
     * scene at build index `i`. If `i` is 20 (default)
     */
    private IEnumerator DefaultFadeOut(int i = 20)
    {
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
     */
    public IEnumerator GameSceneFadeIn()
    {
        yield return new WaitForSeconds(1f);

        // this for loop runs the countdown before the game starts
        for (int i = 3; i > 0; i--)
        {
            timer.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        timer.text = "Go!";
        AS.Play(); // Activates the music once the countdown finishes
        img.CrossFadeAlpha(0, .5f, false);

        yield return new WaitForSeconds(.5f);

        timer.text = "";
        GameFlowController.main.BeginGame();
    }
}
