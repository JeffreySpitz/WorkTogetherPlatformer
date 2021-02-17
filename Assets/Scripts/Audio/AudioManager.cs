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
        
    }
}
