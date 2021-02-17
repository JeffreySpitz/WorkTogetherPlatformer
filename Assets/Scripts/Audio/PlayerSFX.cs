using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerSFX : MonoBehaviour
{
    //public List<AudioClip> defaultFootsteps = new List<AudioClip>();
    public List<PlayerSFXMaterials> footSteps = new List<PlayerSFXMaterials>();
    public List<PlayerSFXMaterials> jumpSounds = new List<PlayerSFXMaterials>();
    public List<PlayerSFXMaterials> landingSounds = new List<PlayerSFXMaterials>();

    public AudioSource playerSFXEmitter;

    // Start is called before the first frame update
    void Start()
    {
        playerSFXEmitter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShot(AudioClip _audioClip)
    {
        playerSFXEmitter.PlayOneShot(_audioClip);
    }
}
