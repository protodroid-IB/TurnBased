using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource[] audioSources;
    private bool justChanged = false;

    private float volumeChangeRate = 0.5f;

    private float channelOneVol, channelTwoVol;

	// Use this for initialization
	void Start ()
    {
        audioSources = GetComponents<AudioSource>();

        channelOneVol = 1f;
        channelTwoVol = 0f;

        audioSources[0].volume = channelOneVol;
        audioSources[1].volume = channelTwoVol;

        audioSources[0].Play();
        audioSources[1].Stop();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameController.gameState == GameState.Overworld)
        {
            if (audioSources[0].isPlaying == false) audioSources[0].Play();

            channelOneVol += volumeChangeRate * Time.deltaTime;
            channelTwoVol -= volumeChangeRate * Time.deltaTime;

            if (channelOneVol >= 1f) channelOneVol = 1f;

            if (channelTwoVol <= 0f)
            {
                channelTwoVol = 0f;
                audioSources[1].Stop();
            }

            audioSources[0].volume = channelOneVol;
            audioSources[1].volume = channelTwoVol;
        }
        else if(GameController.gameState == GameState.Battle)
        {
            if (audioSources[1].isPlaying == false) audioSources[1].Play();

            channelOneVol -= volumeChangeRate * Time.deltaTime;
            channelTwoVol += volumeChangeRate * Time.deltaTime;

            if (channelTwoVol >= 1f) channelTwoVol = 1f;

            if (channelOneVol <= 0f)
            {
                channelOneVol = 0f;
                audioSources[0].Stop();
            }
            audioSources[0].volume = channelOneVol;
            audioSources[1].volume = channelTwoVol;
        }
	}
}
