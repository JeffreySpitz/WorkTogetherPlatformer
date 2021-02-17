﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;




public class AmbienceManager : MonoBehaviour
{
    public List<AudioSource> ambienceEmitters = new List<AudioSource>();
    public AudioClip altAmbience;
    private AudioClip defaultSceneAmbience;

    private AudioSource ambienceEmitter1;
    private AudioSource ambienceEmitter2;
    private AudioSceneSetup audioSceneSetup;

    public float defaultAmbienceAudioSourceVolume = 0.5f;

    public AudioMixerGroup defaultAmbienceAudioMixerGroup;

    public AudioClip currentlyPlayingAmbience;

    public AudioSource nonPlayingAmbienceEmitter;




    // Start is called before the first frame update
    void Start()
    {
        ambienceEmitter1 = gameObject.AddComponent<AudioSource>();
        ambienceEmitter2 = gameObject.AddComponent<AudioSource>();

        List<AudioSource> ambEmitter = new List<AudioSource>();
        ambEmitter.Add(ambienceEmitter1);
        ambEmitter.Add(ambienceEmitter2);

        foreach (AudioSource _audioSource in ambEmitter)
        {
            _audioSource.loop = true;
            _audioSource.playOnAwake = false;
            _audioSource.volume = defaultAmbienceAudioSourceVolume;
           
        }

        ambienceEmitter1.outputAudioMixerGroup = defaultAmbienceAudioMixerGroup;

        audioSceneSetup = FindObjectOfType<AudioSceneSetup>();

        defaultSceneAmbience = audioSceneSetup.sceneDefaultAmbience;

    }

    // Update is called once per frame
    void Update()
    {

        if (!ambienceEmitter1.isPlaying && !ambienceEmitter2.isPlaying)
        {
            if (defaultSceneAmbience == null) { return; }

            ambienceEmitter1.clip = defaultSceneAmbience;
            ambienceEmitter1.loop = true;
            ambienceEmitter1.Play();
        }
    }

    public void AmbienceCurrentlyPlaying()
    {
        if (ambienceEmitter1.isPlaying && ambienceEmitter1.loop == true)
        {
            currentlyPlayingAmbience = ambienceEmitter1.clip;
        }
        else if (ambienceEmitter2.isPlaying && ambienceEmitter2.loop == true)
        {
            currentlyPlayingAmbience = ambienceEmitter2.clip;
        }
        else
        {
            currentlyPlayingAmbience =  null;
        }    
    }

    private void ChooseNextEmitterToPlay()
    {
        if(!ambienceEmitter1.isPlaying)
        {
            nonPlayingAmbienceEmitter = ambienceEmitter1;
        } 
        else if (!ambienceEmitter2.isPlaying)
        {
            nonPlayingAmbienceEmitter = ambienceEmitter2;
        }

    }
}
