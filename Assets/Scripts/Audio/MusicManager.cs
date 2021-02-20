using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource musicEmitter;
    public List<AudioClip> musicManagerPlaylist = new List<AudioClip>();
    private AudioSceneSetup audioSceneSetup;

    public AudioMixerSnapshot defaultMusicSnapshot;
    public AudioMixerSnapshot silenceMusicSnapshot;


    // Start is called before the first frame update
    void Start()
    {
        musicEmitter = GetComponent<AudioSource>();

        audioSceneSetup = FindObjectOfType<AudioSceneSetup>();
        musicManagerPlaylist.Clear();
        musicManagerPlaylist = audioSceneSetup.sceneMusicPlaylist;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!musicEmitter.isPlaying)
        {
            if(musicManagerPlaylist == null) { return; }

            musicEmitter.clip = musicManagerPlaylist[0];
            musicEmitter.loop = true;
            musicEmitter.Play();
        }
    }

    public void StartLevelMusic()
    {

    }
    public void EndLevelMusic()
    {
        silenceMusicSnapshot.TransitionTo(2f);
    }
}
