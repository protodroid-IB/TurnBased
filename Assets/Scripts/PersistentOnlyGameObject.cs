using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistentOnlyGameObject : MonoBehaviour
{

    // boolean that checks if the gameobejct this script is attached to exists or not
    private bool gameControllerDoesExist = false;

    // Use this for initialization
    void Awake ()
    {
        CheckGameControllerExists();

        SceneFader sceneFader = GameObject.FindWithTag("SceneFader").GetComponent<SceneFader>();

        if(sceneFader.GetCurrentSceneName().Equals("MainMenu"))
        {
            Destroy(gameObject);
        }
    }

    private void CheckGameControllerExists()
    {
        // if the does not exist
        if (gameControllerDoesExist == false)
        {
            // do not destroy this gameobject when a new scene is loaded
            DontDestroyOnLoad(this.gameObject);

            // this gameobject does exist
            gameControllerDoesExist = true;
        }

        // if more than one of this type of object exists
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // destroy the gameobject
            Destroy(gameObject);
        }
    }
}
