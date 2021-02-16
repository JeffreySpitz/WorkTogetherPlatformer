using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class AmbienceManager : MonoBehaviour
{
    private AudioSource ambienceEmitter;
    private AudioSceneSetup audioSceneSetup;
    public List<AudioClip> ambienceManagerPlaylist = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        ambienceEmitter = GetComponent<AudioSource>();
        audioSceneSetup = FindObjectOfType<AudioSceneSetup>();
        ambienceManagerPlaylist.Clear();
        ambienceManagerPlaylist = audioSceneSetup.sceneAmbiencePlaylist;
    }

    // Update is called once per frame
    void Update()
    {

        if (!ambienceEmitter.isPlaying)
        {
            if (ambienceManagerPlaylist == null) { return; }

            ambienceEmitter.clip = ambienceManagerPlaylist[0];
            ambienceEmitter.loop = true;
            ambienceEmitter.Play();
        }
    }
}
