using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/*
*
*	Scipt Name: SceneFader.cs
*	Script Author: Laurence Valentini
*
*	Script Summary: This script handles the fades between scenes and the splash screen function
*/


public class SceneFader : MonoBehaviour
{


    /*
	*
	*	SERIALIZED FIELDS
	*
	*/

    // reference to the scene faders animator
    private Animator animator;

    /*
	*
	*	HIDDEN FIELDS
	*
	*/


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // string of the scene to change to
    private string sceneToChangeTo;


    // this function starts fading the scene using the animator.
    public void FadeIntoScene(string inSceneName)
    {
        sceneToChangeTo = inSceneName;
        animator.SetTrigger("FadeOut");
    }


    // this function is called when an animation is complete and loads a new scene
    public void FadeComplete()
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void RestartScene()
    {
        FadeIntoScene(SceneManager.GetActiveScene().name);
    }


    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void Quit()
    {
        Application.Quit();
    }


}
