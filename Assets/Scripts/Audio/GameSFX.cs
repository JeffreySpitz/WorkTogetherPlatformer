﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class GameSFX : MonoBehaviour
{
    [SerializeField] bool debugOn = false;

    [Header("Game SFX Audio Clips")]
    public AudioClip levelStartSound;
    public AudioClip levelEndSound;
    public AudioClip playerDeathSound;
    public AudioClip playerSwitchSound;
    public AudioClip PressButtonSFX;


    private AudioSource gameSFXEmitter;

    // Start is called before the first frame update
    void Start()
    {
        gameSFXEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameSFX(string _EventName)
    {
        if(_EventName == "levelStart" || _EventName == "LevelStart")
        {
            if (levelStartSound == null) 
            {   
                if(debugOn)
                {
                    Debug.Log("Level start sound == null - RETURED");
                }
                return; 
            }
            gameSFXEmitter.PlayOneShot(levelStartSound);
        }
        else if (_EventName == "levelEnd" || _EventName == "LevelEnd")
        {
            if (levelEndSound == null)
            {
                if (debugOn)
                {
                    Debug.Log("Level end sound == null - RETURED");
                }
                return;
            }
            gameSFXEmitter.PlayOneShot(levelEndSound);
        }
        else if (_EventName == "playerDeath" || _EventName == "PlayerDeath")
        {
            if (playerDeathSound == null)
            {
                if (debugOn)
                {
                    Debug.Log("Player death sound == null - RETURED");
                }
                return;
            }
            gameSFXEmitter.PlayOneShot(playerDeathSound);
        }
        else if (_EventName == "playerSwitch" || _EventName == "PlayerSwitch")
        {
            if (playerSwitchSound == null)
            {
                if (debugOn)
                {
                    Debug.Log("Player Switch sound == null - RETURED");
                }
                return;
            }
            gameSFXEmitter.PlayOneShot(playerSwitchSound);
        }
        else if (_EventName == "buttonSFX" || _EventName == "ButtonSFX")
        {
            if (PressButtonSFX == null)
            {
                if (debugOn)
                {
                    Debug.Log("Player Switch sound == null - RETURED");
                }
                return;
            }
            gameSFXEmitter.PlayOneShot(PressButtonSFX);
        }



        else
        {
            if(debugOn)
            {
                Debug.Log("GameSFX.cs - PlayGameSFX(string _eventName - _eventName does not match any GameSFX event - NO ACTION TAKEN");
            }
        }

        
    }
}
