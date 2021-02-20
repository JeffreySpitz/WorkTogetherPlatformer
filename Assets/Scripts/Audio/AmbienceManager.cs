using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;




public class AmbienceManager : MonoBehaviour
{
    public List<AudioSource> ambienceEmitters = new List<AudioSource>();
   
    private AudioClip defaultSceneAmbience;

    private AudioSource ambienceEmitter1;
    private AudioSource ambienceEmitter2;
    private AudioSceneSetup audioSceneSetup;

    [SerializeField] float defaultAmbVolume = 1.0f;

    public AudioMixerGroup defaultAmbMixer;
    public AudioMixerGroup altAmbMixer;

    public AudioClip currentlyPlayingAmbience;

    public AudioSource nonPlayingAmbienceEmitter;

    public AudioMixerSnapshot defaultAmbienceSnapshot;
    public AudioMixerSnapshot altAmbienceSnapshot;
    public AudioMixerSnapshot silenceAmbienceSnapshot;




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
            _audioSource.volume = defaultAmbVolume;
           
        }

        ambienceEmitter1.outputAudioMixerGroup = defaultAmbMixer;
        ambienceEmitter2.outputAudioMixerGroup = altAmbMixer;

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
        else
        {
            Debug.Log("AmbienceManager.cs - both AudioEmittersare playing - no free emitter to chosse");
        }

    }

    public void PlayAltAmbienceFromTrigger(AudioClip _altAmbienceClip)
    {
        ChooseNextEmitterToPlay();
        nonPlayingAmbienceEmitter.clip = _altAmbienceClip;
        nonPlayingAmbienceEmitter.loop = true;
        nonPlayingAmbienceEmitter.Play();

        altAmbienceSnapshot.TransitionTo(2f);
    }

    public void PlayDefaultFromTrigger()
    {
        defaultAmbienceSnapshot.TransitionTo(2f);
    }

    public void EndLevelAmbience()
    {
        silenceAmbienceSnapshot.TransitionTo(2f);
    }

    public void StartLevelAmbience()
    {

    }
} 


