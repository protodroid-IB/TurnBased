using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameObject : MonoBehaviour
{

    // boolean that checks if the gameobejct this script is attached to exists or not
    private bool goExists = false;

    // Use this for initialization
    void Awake()
    {
        CheckGameObjectExists();
    }

    private void CheckGameObjectExists()
    {
        // if the does not exist
        if (goExists == false)
        {
            // do not destroy this gameobject when a new scene is loaded
            DontDestroyOnLoad(this.gameObject);

            // this gameobject does exist
            goExists = true;
        }
    }

}
