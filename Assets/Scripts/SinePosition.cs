using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinePosition : MonoBehaviour
{




    /* 
     * File: 			SinePosition.cs
     * Author:          Laurence Valentini
     * Description: 	A script that allows control over the oscillating of a gameobjects position on up to 3 axes independantly using a sine wave
     */







    /* * * * * * * * * * * * * * * * * * * * * * * *
     * *                                         * *
                PUBLIC MEMBER VARIABLES
     * *                                         * *
     * * * * * * * * * * * * * * * * * * * * * * * */


    // struct used for a sine wave properties
    [System.Serializable]
    public struct SineWave
    {
        public float amplitude;         // the amplitude
        public float angularFrequency;  // the frequency
        public float phaseShift;        // horizontal shift
        public float verticalShift;     // vertical shift
    }


    public bool xActive;    // switch to activate sine wave manipulations in the x-axis
    public SineWave xSine;  // sine wave for the x-axis

    
    public bool yActive;    // switch to activate sine wave manipulations in the y-axis
    public SineWave ySine;  // sine wave for the y-axis


    public bool zActive;    // switch to activate sine wave manipulations in the z-axis
    public SineWave zSine;  // sine wave for the z-axis







    /* * * * * * * * * * * * * * * * * * * * * * * *
     * *                                         * *
                PRIVATE MEMBER VARIABLES
     * *                                         * *
     * * * * * * * * * * * * * * * * * * * * * * * */



    //initial position of the gameobject when instantiated
    private Vector3 initialPosition;

    //reference for a rect transform if a UI object
    private RectTransform rectTransform;

    //adjustments to be made to initial position in each axis
    private float xPos;
    private float yPos;
    private float zPos;










    void Start()
    {
        //try to grab a reference to rect transform component
        rectTransform = GetComponent<RectTransform>();

        //if rect transform component exists within the gameobject
        if (rectTransform)
        {
            //grab the position info from the rect transform and set it to the objects position when instantiated
            initialPosition = rectTransform.anchoredPosition3D;
        }
        else
        {
            //grab the position info from the transform and set it to the objects position when instantiated
            initialPosition = transform.position;
        }
    }







    // Update is called once per frame
    void Update()
    {
        //with respect to time, finds the necessary adjustments needed to make to the positon of the game object this update
        SineWaveAdjustments();

        //makes the changes to the position of the gameobject
        UpdatePosition(); 
    }





    // THIS METHOD FINDS WHAT ADJUSTMENTS ARE TO BE MADE TO THE POSITION OF A GAMEOBJECT, USING A SINE WAVE WITH RESPECT TO TIME
    // it does so for each axis but doesn't apply the changes to the objects position
    private void SineWaveAdjustments()
    {
        //if the object is to oscillate on the x axis
        if (xActive)
        {
            // using a sine wave, finds the difference in position on the x-axis with respect to time
            xPos = xSine.amplitude * Mathf.Sin(xSine.angularFrequency * Time.time + xSine.phaseShift) + xSine.verticalShift;
        }

        //if the object is to oscillate on the y axis
        if (yActive)
        {
            // using a sine wave, finds the difference in position on the y-axis with respect to time
            yPos = ySine.amplitude * Mathf.Sin(ySine.angularFrequency * Time.time + ySine.phaseShift) + ySine.verticalShift;
        }

        //if the object is to oscillate on the z axis
        if (zActive)
        {
            // using a sine wave, finds the difference in position on the z-axis with respect to time
            zPos = zSine.amplitude * Mathf.Sin(zSine.angularFrequency * Time.time + zSine.phaseShift) + zSine.verticalShift;
        }
    }







    // THIS METHOD FINDS UPDATES THE POSITION OF THE GAMEOBJECT BASED ON THE ADJUSTMENTS DETERMINED IN xPos, yPos, zPos
    // this method determines whether these adjustments neeed to made to a transform or rect transform and then does so accordingly
    private void UpdatePosition()
    {
        //if rect transfrom exists
        if (rectTransform)
        {
            //set the rect transforms scale to initial position plus changes based on sine waves above
            rectTransform.anchoredPosition3D = new Vector3(initialPosition.x + xPos, initialPosition.y + yPos, initialPosition.z + zPos);
        }
        else
        {
            //set the transforms scale to initial position plus changes based on sine waves above
            transform.position = new Vector3(initialPosition.x + xPos, initialPosition.y + yPos, initialPosition.z + zPos);
        }
    }
}
