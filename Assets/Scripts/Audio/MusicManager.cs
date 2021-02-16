using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource musicEmitter;
    public List<AudioClip> musicManagerPlaylist = new List<AudioClip>();
    private AudioSceneSetup audioSceneSetup;

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
}
