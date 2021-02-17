using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private MusicManager musicManager;
    private AmbienceManager ambienceManager;
    private GameSFX gameSFX;
    private PlayerSFX playerSFX;

    public AudioClip sceneDefaultMusic;
    public AudioClip sceneDefaultAmbience;

    public bool muteMusic = false;
    public bool muteAmb = false;
    public bool muteGameSFX = false;
    public bool mutePlayerSFX = false;





    void Start()
    {
        musicManager = GetComponentInChildren<MusicManager>();
        ambienceManager = GetComponentInChildren<AmbienceManager>();
        gameSFX = GetComponentInChildren<GameSFX>();
        playerSFX = GetComponentInChildren<PlayerSFX>();

        musicManager.GetComponent<AudioSource>().clip = sceneDefaultMusic;
      
    }

     
    void Update()
    {
        if(muteMusic == true)
        {
            musicManager.GetComponent<AudioSource>().mute = true;
        }

        if (muteAmb == true)
        {
            ambienceManager.GetComponent<AudioSource>().mute = true;
        }

        if (muteGameSFX == true)
        {
            gameSFX.GetComponent<AudioSource>().mute = true;
        }
        
        if (mutePlayerSFX == true)
        {
            playerSFX.GetComponent<AudioSource>().mute = true;
        }
    }
}
